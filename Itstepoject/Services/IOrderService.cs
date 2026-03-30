using Itstepoject.Models;

namespace Itstepoject.Services
{
    public enum RemoveFromCartResult
    {
        NotFound = 0,
        NotInCart = 1,
        Deleted = 2
    }

    public interface IOrderService
    {
        Task<IReadOnlyList<Order>> GetAllAsync(string userId, CancellationToken cancellationToken = default);
        Task<Order?> GetByIdAsync(string userId, int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Order>> GetCartAsync(string userId, CancellationToken cancellationToken = default);
        Task<Order> CreateAsync(string userId, Order order, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(string userId, Order order, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string userId, int id, CancellationToken cancellationToken = default);
        Task<RemoveFromCartResult> RemoveFromCartAsync(string userId, int id, CancellationToken cancellationToken = default);
    }
}
