using Itstepoject.Models;

namespace Itstepoject.Services
{
    public interface IMenuService
    {
        Task<IReadOnlyList<MenuItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<MenuItem> CreateAsync(MenuItem item, CancellationToken cancellationToken = default);
        Task UpdateAsync(MenuItem item, CancellationToken cancellationToken = default);
    }
}
