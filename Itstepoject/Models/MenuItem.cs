using System.ComponentModel.DataAnnotations;

namespace Itstepoject.Models
{
    public class MenuItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; }

        [Range(0.01, 1_000_000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public required string Description { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
