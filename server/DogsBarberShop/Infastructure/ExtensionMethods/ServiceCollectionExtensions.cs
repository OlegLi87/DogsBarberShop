using DogsBarberShop.Entities.Models;
using DogsBarberShop.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DogsBarberShop.Infastructure.ExtensionMethods
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<DogsBarberShopDbContext>(opts =>
            {
                // By default migrations will be created in the folder containing DbContext class,so the seconcd argument can be omitted.
                opts.UseSqlServer(connectionString, x => x.MigrationsAssembly("DogsBarberShop.Persistence"));
                opts.EnableSensitiveDataLogging();
            });
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<DogsBarberShopDbContext>();

            return services;
        }
    }
}