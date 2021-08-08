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
    }
}