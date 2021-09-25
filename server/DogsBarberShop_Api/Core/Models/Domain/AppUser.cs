using System;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop_Api.Core.Models.Domain
{
    public class AppUser : IdentityUser
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}