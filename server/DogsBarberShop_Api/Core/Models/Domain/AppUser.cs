using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop_Api.Core.Models.Domain
{
    public class AppUser : IdentityUser
    {
        public virtual Order Order { get; set; }
    }
}