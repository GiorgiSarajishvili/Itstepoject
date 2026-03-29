using System.ComponentModel.DataAnnotations;

namespace Itstepoject.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
