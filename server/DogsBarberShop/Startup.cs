using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Infastructure.ExtensionMethods;
using DogsBarberShop.Services.AuthService;
using DogsBarberShop.Services.JwtService;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DogsBarberShop
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config) => _config = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_config);

            services.ConfigureDbContext();
            services.ConfigureIdentity();
            services.ConfigureJwtAuthentication();
            services.ConfigureCors();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService<User>, JwtService>();
            services.AddScoped<IUtilsService, UtilsService>();

            services.AddHttpContextAccessor(); // for accessing HttpContext in custom components

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettings> settingsOpts)
        {
            var appSettings = settingsOpts.Value;

            app.UseCors(appSettings.Cors.PolicyName);

            app.UseRouting();
            app.UseAuthentication();

            app.ConfigureEndpoints();
        }
    }
}
