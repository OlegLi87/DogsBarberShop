using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Entities.Models
{
    public class User : IdentityUser
    {
        public IEnumerable<Pet> Pets { get; set; }
    }
}