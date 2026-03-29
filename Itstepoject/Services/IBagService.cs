using Itstepoject.Models;

namespace Itstepoject.Services
{
    public interface IBagService
    {
        Task<IReadOnlyList<Bag>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Bag?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Bag> CreateAsync(Bag item, CancellationToken cancellationToken = default);
        Task UpdateAsync(Bag item, CancellationToken cancellationToken = default);
    }
}
