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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Pet>()
                   .HasOne<User>(p => p.User)
                   .WithMany(u => u.Pets)
                   .HasForeignKey(p => p.UserId);

            builder.Entity<Pet>()
                   .HasOne<Order>(p => p.Order)
                   .WithOne(o => o.Pet)
                   .HasForeignKey<Order>(o => o.PetId);
        }

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