using DogsBarberShop_Api.Core.Repository;

namespace DogsBarberShop_Api.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IOrdersRepository Orders { get; set; }
        public UnitOfWork(DogsBarberShopDbContext dbContext) => Orders = new OrdersRepository(dbContext);
    }
}