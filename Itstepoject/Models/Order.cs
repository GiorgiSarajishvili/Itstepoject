using System.ComponentModel.DataAnnotations;

namespace Itstepoject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
