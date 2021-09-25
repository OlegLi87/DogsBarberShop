using System;
using System.Text;
using DogsBarberShop_Api.Core.Models.Domain;
using DogsBarberShop_Api.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;

namespace DogsBarberShop_Api.Infastructure.ExtensionMethods
{
    public static class ServicesConfigurations
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configs)
        {
            LoggerFactory loggerFactory = new LoggerFactory(new[] { new NLogLoggerProvider() });

            services.AddDbContext<DogsBarberShopDbContext>(opts =>
            {
                opts.UseSqlServer(configs.GetConnectionString("DogsBarberShopDb"));
                opts.EnableSensitiveDataLogging();
                opts.UseLoggerFactory(loggerFactory);
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireDigit = false;
                opts.Password.RequiredUniqueChars = 0;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<DogsBarberShopDbContext>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            var appSettings = getAppSettings(services);
            var jwtSecretBytes = Encoding.ASCII.GetBytes(appSettings.JwtSecret);

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSecretBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            var appSettings = getAppSettings(services);

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
        }

        private static AppSettings getAppSettings(IServiceCollection services)
        {
            var opts = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<IOptions<AppSettings>>();
            return opts.Value as AppSettings;
        }
    }
}