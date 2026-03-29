using System.ComponentModel.DataAnnotations;

namespace Itstepoject.Models
{
    public class Accessory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }

        public required string Material { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
