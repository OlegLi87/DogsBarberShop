using System;
using System.Threading.Tasks;
using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop.Persistence.Repositories
{
    public class PetsRepository : GenericRepository<Pet>, IPetsRepository
    {
        public PetsRepository(DbContext context) : base(context)
        {
        }
    }
}