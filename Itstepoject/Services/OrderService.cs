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

        public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Loading all orders");
            return await _db.Orders.AsNoTracking().OrderByDescending(o => o.OrderDate).ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created order {Id} for {Customer}", order.Id, order.CustomerName);
            return order;
        }

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Updated order {Id}", order.Id);
        }
    }
}
