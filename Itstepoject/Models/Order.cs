using System.ComponentModel.DataAnnotations;

namespace Itstepoject.Models
{
    public enum OrderStatus
    {
        Cart = 0,
        Ordered = 1
    }

    public class Order
    {
        public int Id { get; set; }
        public required string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Cart;
        // Belongs to a specific user (IdentityUser.Id).
        // Nullable to keep existing rows compatible with migrations.
        public string? UserId { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
