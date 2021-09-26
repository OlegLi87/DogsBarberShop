using System.Collections.Generic;

namespace DogsBarberShop.Entities.InfastructureModels
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public ConnectionStringsSettings ConnectionStrings { get; set; }
        public CorsSettings Cors { get; set; }
        public IEnumerable<string> ApplicationUrls { get; set; }
        public struct ConnectionStringsSettings
        {
            public string DogsBarberShop_Dev { get; set; }
            public string DogsBarberShop_Prod { get; set; }

            public string this[string value]
            {
                get
                {
                    if (value.ToLower().Contains("dev"))
                        return DogsBarberShop_Dev;
                    else if (value.ToLower().Contains("prod"))
                        return DogsBarberShop_Prod;
                    else return string.Empty;
                }
            }
        }
        public struct CorsSettings
        {
            public string PolicyName { get; set; }
            public IEnumerable<string> AllowedOrigins { get; set; }
        }
    }
}
