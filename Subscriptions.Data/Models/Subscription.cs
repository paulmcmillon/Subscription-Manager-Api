using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscriptions.Data.Models
{
    [Index(nameof(Email), IsUnique = true, Name = "IX_Unique_Email")]
    [Table("Subscriptions")]
    public sealed class Subscription
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        [Required]
        public required int SubscriptionTypeId { get; set; }
        [Required]
        public required string BillingInterval { get; set; } = "Monthly";
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required, MinLength(5), MaxLength(100)]
        public required string AdministratorName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateOnly PurchaseDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);
        [Required]
        public DateOnly NextPaymentDate { get; internal set; }
        [Required, DefaultValue(false)]
        public required bool IsLocked { get; set; } = false;
        [Required]
        public required string LockReason { get; set; } = "Unlocked";
        [Required, DefaultValue(false)]
        public required bool IsSoftDeleted { get; set; } = false;
        public SubscriptionType? Type { get; internal set; }
        public IEnumerable<Payment>? Payments { get; set; }

        private DateOnly CalculateNextPaymentDate => BillingInterval switch
        {
            "Monthly" => NextPaymentDate.AddMonths(1),
            "Annually" => NextPaymentDate.AddYears(1),
            _ => throw new ArgumentOutOfRangeException(nameof(BillingInterval), $"Invalid billing interval: {BillingInterval}")
        };
    }

    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasMany(t => t.Payments).WithOne().HasForeignKey("SubscriptionId");

            ///<summary>
            ///This is a check constraint on the database column to ensure that the
            ///BillingInterval value can only contain listed values.
            /// </summary>
            builder.ToTable(t => t.HasCheckConstraint("CK_Subscription_BillingInterval", "BillingInterval IN ('Monthly', 'Annually')"));

            ///<summary>
            ///This is a check constraint on the database column to ensure that the
            ///LockReason value can only contain listed values.
            ///</summary>
            builder.ToTable(t => t.HasCheckConstraint("CK_Subscription_LockReason", "LockReason IN ('Unlocked', 'PendingPayment', 'SubscriptionExpired')"));
        }
    }
}
