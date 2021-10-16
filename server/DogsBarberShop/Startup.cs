using System.IO;
using DogsbarberShop.Controllers.Filters;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Infastructure.ExtensionMethods;
using DogsBarberShop.Persistence;
using DogsBarberShop.Services.AuthService;
using DogsBarberShop.Services.EmailService;
using DogsBarberShop.Services.JwtService;
using DogsBarberShop.Services.UnitOfWork;
using DogsBarberShop.Services.UtilsService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
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
            services.ConfigureMvc();
            services.ConfigureAutoMapper();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService<User>, JwtService>();
            services.AddScoped<IUtilsService, UtilsService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<AuthActionFilter>();
            services.AddScoped<UplaodImageSizeLimitResourceFilter>();

            services.AddHttpContextAccessor(); // for accessing HttpContext in custom components
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<AppSettings> settingsOpts)
        {
            var appSettings = settingsOpts.Value;

            app.UseCors(appSettings.Cors.PolicyName);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), appSettings.UploadImage.ImagesPath)),

                RequestPath = '/' + appSettings.UploadImage.ImagesPath
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureEndpoints();

            if (env.IsDevelopment() || appSettings.Migrate)
                SeedDb.Migrate(app.ApplicationServices.CreateScope().ServiceProvider.GetService<DogsBarberShopDbContext>());
        }
    }
}
