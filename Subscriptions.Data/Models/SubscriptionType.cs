using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscriptions.Data.Models
{
    [Table ("SubscriptionTypes")]
    public sealed class SubscriptionType
    {
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        [Required, MinLength(5), MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; } = null;
        [Required]
        [Column(TypeName ="smallmoney")]
        public required decimal PriceMonthly { get; set; } = 0.0m;
        [Required]
        [Column(TypeName = "smallmoney")]
        public required decimal PriceYearly { get; set; } = 0.0m;

        public IEnumerable<Feature>? Features { get; set; }
    }

    public class SubscriptionTypeConfiguration : IEntityTypeConfiguration<SubscriptionType>
    {
        public void Configure(EntityTypeBuilder<SubscriptionType> builder)
        {
            builder.HasMany(s => s.Features).WithOne().HasForeignKey("SubscriptionTypeId");
        }
    }
}
