using DogsBarberShop_Api.Core.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop_Api.Persistence
{
    public class DogsBarberShopDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DogsBarberShopDbContext(DbContextOptions opts) : base(opts)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                   .HasOne(u => u.Order)
                   .WithOne(o => o.AppUser)
                   .HasForeignKey<Order>(o => o.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}