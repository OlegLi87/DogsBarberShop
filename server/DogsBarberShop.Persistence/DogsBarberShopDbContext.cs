using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.DomainModels.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop.Persistence
{
    public class DogsBarberShopDbContext : IdentityDbContext<User>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pet> Pets { get; set; }

        public DogsBarberShopDbContext(DbContextOptions<DogsBarberShopDbContext> opts) : base(opts)
        { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var trackables = ChangeTracker.Entries<IDateTrackableEntity>()
                                         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in trackables)
                entry.Entity.CreationDate = DateTime.Now;

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}