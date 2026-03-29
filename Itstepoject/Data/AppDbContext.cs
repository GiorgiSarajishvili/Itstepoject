using Itstepoject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Itstepoject.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Bag> Bags => Set<Bag>();
        public DbSet<Accessory> Accessories => Set<Accessory>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MenuItem>().Property(m => m.Price).HasPrecision(18, 2);
            builder.Entity<Bag>().Property(b => b.Price).HasPrecision(18, 2);
            builder.Entity<Bag>().Property(b => b.CapacityLiters).HasPrecision(18, 2);
            builder.Entity<Accessory>().Property(a => a.Price).HasPrecision(18, 2);
        }
    }
}
