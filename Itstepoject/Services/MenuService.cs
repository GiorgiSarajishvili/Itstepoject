using Itstepoject.Data;
using Itstepoject.Models;
using Microsoft.EntityFrameworkCore;

namespace Itstepoject.Services
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<MenuService> _logger;

        public MenuService(AppDbContext db, ILogger<MenuService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IReadOnlyList<MenuItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("Loading all menu items");
            return await _db.MenuItems.AsNoTracking().OrderBy(m => m.Name).ToListAsync(cancellationToken);
        }

        public async Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.MenuItems.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<MenuItem> CreateAsync(MenuItem item, CancellationToken cancellationToken = default)
        {
            _db.MenuItems.Add(item);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Created menu item {Id} {Name}", item.Id, item.Name);
            return item;
        }

        public async Task UpdateAsync(MenuItem item, CancellationToken cancellationToken = default)
        {
            _db.MenuItems.Update(item);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Updated menu item {Id}", item.Id);
        }
    }
}
