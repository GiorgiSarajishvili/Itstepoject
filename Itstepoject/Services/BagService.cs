using Itstepoject.Data;
using Itstepoject.Models;
using Microsoft.EntityFrameworkCore;

namespace Itstepoject.Services
{
    public class BagService : IBagService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<BagService> _logger;

        public BagService(AppDbContext db, ILogger<BagService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Bag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Loading all bags");
            return await _db.Bags.AsNoTracking().OrderBy(b => b.Name).ToListAsync(cancellationToken);
        }

        public async Task<Bag?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Bags.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<Bag> CreateAsync(Bag item, CancellationToken cancellationToken = default)
        {
            _db.Bags.Add(item);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created bag {Id} {Name}", item.Id, item.Name);
            return item;
        }

        public async Task UpdateAsync(Bag item, CancellationToken cancellationToken = default)
        {
            _db.Bags.Update(item);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Updated bag {Id}", item.Id);
        }
    }
}
