using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public async override Task<Order> GetById(Guid id)
        {
            return await TargetData.Include(o => o.Pet)
                                   .Include(o => o.User)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async override Task<List<Order>> Get(Expression<Func<Order, bool>> predicate = null)
        {
            if (predicate is null)
                predicate = entity => true;

            return await TargetData.Include(o => o.Pet)
                                   .Include(o => o.User)
                                   .Where(predicate)
                                   .AsNoTracking()
                                   .ToListAsync();
        }
    }
}