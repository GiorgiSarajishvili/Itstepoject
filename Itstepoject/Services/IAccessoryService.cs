using Itstepoject.Models;

namespace Itstepoject.Services
{
    public interface IAccessoryService
    {
        Task<IReadOnlyList<Accessory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Accessory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Accessory> CreateAsync(Accessory item, CancellationToken cancellationToken = default);
        Task UpdateAsync(Accessory item, CancellationToken cancellationToken = default);
    }
}
