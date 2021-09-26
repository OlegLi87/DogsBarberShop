using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DogsBarberShop.Entities.DomainModels
{
    public class User : IdentityUser
    {
        public IEnumerable<Pet> Pets { get; set; }
    }
}