using Itstepoject.Models;

namespace Itstepoject.Services
{
    public interface IOrderService
    {
        Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
    }
}
