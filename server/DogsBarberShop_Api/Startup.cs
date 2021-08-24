using System;
using DogsBarberShop_Api.Core.Services.AuthService;
using DogsBarberShop_Api.Infastructure;
using DogsBarberShop_Api.Infastructure.ExtensionMethods;
using DogsBarberShop_Api.Infastructure.Filters;
using DogsBarberShop_Api.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DogsBarberShop_Api
{
    public class Startup
    {
        private readonly IConfiguration _configs;
        public Startup(IConfiguration configs) => _configs = configs;

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_configs);

            services.ConfigureDbContext(_configs);
            services.ConfigureIdentity();
            services.ConfigureAuthentication();
            services.ConfigureCors();

            services.AddScoped<IAuthService, AuthService>();

            services.AddMvc(opts =>
            {
                opts.Filters.Add(typeof(ResultFilterAttribute));
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                                   IOptions<AppSettings> opts, DogsBarberShopDbContext dbContext)
        {
            var appSettings = opts.Value as AppSettings;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseCors(appSettings.Cors.PolicyName);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            if (env.IsDevelopment() || (_configs["initDb"] ?? "") == "init")
                InitDb.Migrate(dbContext);
        }
    }
}
