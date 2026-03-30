using Itstepoject.Data;
using Itstepoject.Models;
using Microsoft.EntityFrameworkCore;

namespace Itstepoject.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext db, ILogger<OrderService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Loading all orders");
            return await _db.Orders.AsNoTracking()
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(string userId, int id, CancellationToken cancellationToken = default)
        {
            return await _db.Orders.AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId, cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetCartAsync(string userId, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Loading orders in cart");
            return await _db.Orders.AsNoTracking()
                .Where(o => o.Status == OrderStatus.Cart && o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Order> CreateAsync(string userId, Order order, CancellationToken cancellationToken = default)
        {
            // For this app "cart" means orders that users liked but haven't finalized.
            order.Status = OrderStatus.Cart;
            order.UserId = userId;
            _db.Orders.Add(order);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created order {Id} for {Customer}", order.Id, order.CustomerName);
            return order;
        }

        public async Task<bool> UpdateAsync(string userId, Order order, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Orders.FirstOrDefaultAsync(o => o.Id == order.Id && o.UserId == userId, cancellationToken);
            if (entity is null)
            {
                return false;
            }

            // Keep ownership + status consistent with user's cart flow.
            entity.CustomerName = order.CustomerName;
            entity.OrderDate = order.OrderDate;
            entity.Status = order.Status;

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Updated order {Id}", order.Id);
            return true;
        }

        public async Task<bool> DeleteAsync(string userId, int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId, cancellationToken);
            if (entity is null)
            {
                return false;
            }

            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deleted order {Id}", id);
            return true;
        }

        public async Task<RemoveFromCartResult> RemoveFromCartAsync(string userId, int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId, cancellationToken);
            if (entity is null)
            {
                return RemoveFromCartResult.NotFound;
            }

            if (entity.Status != OrderStatus.Cart)
            {
                return RemoveFromCartResult.NotInCart;
            }

            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Removed order {Id} from cart", id);
            return RemoveFromCartResult.Deleted;
        }
    }
}
