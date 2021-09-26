using Microsoft.AspNetCore.Builder;

namespace DogsBarberShop.Infastructure.ExtensionMethods
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder ConfigureEndpoints(this IApplicationBuilder app)
        {
            return app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}