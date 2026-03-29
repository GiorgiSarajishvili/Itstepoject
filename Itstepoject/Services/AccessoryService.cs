using Itstepoject.Data;
using Itstepoject.Models;
using Microsoft.EntityFrameworkCore;

namespace Itstepoject.Services
{
    public class AccessoryService : IAccessoryService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<AccessoryService> _logger;

        public AccessoryService(AppDbContext db, ILogger<AccessoryService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Accessory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Loading all accessories");
            return await _db.Accessories.AsNoTracking().OrderBy(a => a.Name).ToListAsync(cancellationToken);
        }

        public async Task<Accessory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Accessories.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<Accessory> CreateAsync(Accessory item, CancellationToken cancellationToken = default)
        {
            _db.Accessories.Add(item);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created accessory {Id} {Name}", item.Id, item.Name);
            return item;
        }

        public async Task UpdateAsync(Accessory item, CancellationToken cancellationToken = default)
        {
            _db.Accessories.Update(item);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Updated accessory {Id}", item.Id);
        }
    }
}
