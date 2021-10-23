using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.DomainModels.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
                   .HasKey(p => new { p.Id, p.UserId });

            builder.Entity<Pet>()
                   .Property(p => p.Id)
                   .ValueGeneratedOnAdd(); // since guids are not generated for composite keys.

            builder.Entity<Pet>()
                   .HasOne(p => p.User)
                   .WithMany(u => u.Pets)
                   .HasForeignKey(p => p.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                   .Property(o => o.PetId)
                   .IsRequired(false);

            builder.Entity<Order>()
                   .HasOne(o => o.Pet)
                   .WithOne(p => p.Order)
                   .HasPrincipalKey<Pet>(p => new { p.Id, p.UserId })
                   .HasForeignKey<Order>(o => new { o.PetId, o.UserId })
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
                   .HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
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