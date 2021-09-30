using System.Collections.Generic;

namespace DogsBarberShop.Entities.InfastructureModels
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }  // from user-secrets
        public ConnectionStringsSettings ConnectionStrings { get; set; }
        public CorsSettings Cors { get; set; }
        public IEnumerable<string> ApplicationUrls { get; set; }
        public SmtpSettings Smtp { get; set; }
        public bool Migrate { get; set; }
        public string ConfirmEmailPath { get; set; }
        public string ResetPasswordPath { get; set; }
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

        public struct SmtpSettings
        {
            public string SmtpServer { get; set; }
            public int SmtpPort { get; set; }
            public string SmtpUser { get; set; }  // from user-secrets
            public string SmtpPassword { get; set; }  // from user-secrets
        }
    }
}
