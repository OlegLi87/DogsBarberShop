using System.IO;
using DogsBarberShop.Entities.InfastructureModels;
using DogsBarberShop.Infastructure.ExtensionMethods;
using DogsBarberShop.Persistence;
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
            services.Configure<BussinessData>(_config.GetSection("BussinessData"));

            services.ConfigureDbContext();
            services.ConfigureIdentity();
            services.ConfigureJwtAuthentication();
            services.ConfigureCors();
            services.ConfigureMvc();
            services.ConfigureAutoMapper();
            services.ConfigureScopedServices();

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
