using System;
using System.Text;
using DogsbarberShop.Controllers.Filters;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DogsBarberShop.Infastructure.ExtensionMethods
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        {
            var appSettings = services.GetService<IOptions<AppSettings>>().Value;
            var connectionString = appSettings.ConnectionStrings["dev"];

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
                opts.User.RequireUniqueEmail = true;

                opts.Password.RequiredLength = 5;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<DogsBarberShopDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services)
        {
            var appSettings = services.GetService<IOptions<AppSettings>>().Value;
            var secretBytes = Encoding.ASCII.GetBytes(appSettings.JwtSecret);

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
              {
                  jwt.RequireHttpsMetadata = false;
                  jwt.SaveToken = true;

                  jwt.TokenValidationParameters.ValidateIssuerSigningKey = true;
                  jwt.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(secretBytes);
                  jwt.TokenValidationParameters.ValidateIssuer = true;
                  jwt.TokenValidationParameters.ValidIssuers = appSettings.ApplicationUrls;
                  jwt.TokenValidationParameters.ValidateAudience = true;
                  jwt.TokenValidationParameters.ValidAudiences = appSettings.Cors.AllowedOrigins;
                  jwt.TokenValidationParameters.ValidateLifetime = true;
                  jwt.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
              });

            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            var appSettings = services.GetService<IOptions<AppSettings>>().Value;

            services.AddCors(opts =>
            {
                opts.AddPolicy(appSettings.Cors.PolicyName, builder =>
                {
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();

                    foreach (var origin in appSettings.Cors.AllowedOrigins)
                        builder.WithOrigins(origin);
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(opts =>
            {
                opts.Filters.Add<ModifyResultFilter>();
            });

            return services;
        }

        public static T GetService<T>(this IServiceCollection serviceCollection)
        {
            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                var service = serviceProvider.GetService<T>();
                return service ?? default(T);
            }
        }
    }
}