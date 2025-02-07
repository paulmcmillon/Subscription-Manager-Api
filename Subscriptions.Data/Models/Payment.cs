using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscriptions.Data.Models
{
    [Table("Payments")]
    public sealed class Payment
    {
        [Required]
        public Guid Id { get; init; } = Guid.NewGuid();
        [Required]
        public required Guid SubscriptionId { get; set; }
        [Required]
        [Column(TypeName = "smallmoney")]
        public required decimal Amount { get; set; }
        [Required]
        public DateOnly PaymentDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);

        /// <summary>
        /// Permissable values are PayPal and Stripe.
        /// </summary>
        [Required]
        public required string PaymentMethod { get; set; }
        
        public string? PaymentTransactionId { get; set; } = null;
    }

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            //This is a check constraint on the database column to ensure that the
            //PaymentMethod value can only contain either PayPal or Stripe.
            builder.ToTable(t => t.HasCheckConstraint("CK_Payment_PaymentMethod", "PaymentMethod IN ('PayPal', 'Stripe')"));
        }
    }
}
