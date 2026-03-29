using System.ComponentModel.DataAnnotations;

namespace Itstepoject.Models
{
    public class Bag
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }

        /// <summary>Capacity in liters (e.g. takeaway bag).</summary>
        public decimal CapacityLiters { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
