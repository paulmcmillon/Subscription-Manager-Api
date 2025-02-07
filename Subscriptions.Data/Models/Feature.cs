using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscriptions.Data.Models
{
    [Table("Features")]
    public sealed class Feature
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        [Required]
        public required int SusbcriptionTypeId { get; set; }        
        [Required, MinLength(3), MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; } = null;
    }
}
