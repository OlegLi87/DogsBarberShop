using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DogsBarberShop_Api.Core.Models.Domain;
using DogsBarberShop_Api.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop_Api.Persistence.Repository
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(DbContext dbContext) : base(dbContext)
        { }

        public async override Task<Order> GetById(Guid id)
           => await DbContext.Set<Order>().Include(o => o.AppUser).FirstOrDefaultAsync(o => o.Id == id);

        public async override Task<IEnumerable<Order>> Get(Expression<Func<Order, bool>> predicate = null)
        {
            if (predicate == null)
                predicate = e => true;

            return await DbContext.Set<Order>().Include(o => o.AppUser).Where(predicate).ToListAsync<Order>();
        }
    }
}