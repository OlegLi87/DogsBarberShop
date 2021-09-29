using System;

namespace DogsBarberShop.Entities.InfastructureModels
{
    public struct EmailConfirmationData
    {
        public string UserName { get; set; }
        public string ConfirmationToken { get; set; }
        public string UrlRedirectTo { get; set; }
    }
}