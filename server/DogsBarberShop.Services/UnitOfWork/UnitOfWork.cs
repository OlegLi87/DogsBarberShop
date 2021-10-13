using System.Threading.Tasks;
using DogsBarberShop.Entities.Repositories;
using DogsBarberShop.Persistence;
using DogsBarberShop.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public IOrdersRepository Orders { get; private set; }
        public IPetsRepository Pets { get; private set; }

        public UnitOfWork(DogsBarberShopDbContext context)
        {
            _context = context;

            Orders = new OrdersRepository(context);
            Pets = new PetsRepository(context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}