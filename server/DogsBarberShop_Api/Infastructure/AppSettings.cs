
namespace DogsBarberShop_Api.Infastructure
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public CorsOptions Cors { get; set; }
        public struct CorsOptions
        {
            public string PolicyName { get; set; }
            public string[] AllowedOrigins { get; set; }
        }
    }
}