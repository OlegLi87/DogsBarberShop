using DogsBarberShop.Entities.DomainModels;
using DogsBarberShop.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop.Persistence.Repositories
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext context) : base(context)
        {
        }
    }
}