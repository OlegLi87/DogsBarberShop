using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DogsBarberShop_Api.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop_Api.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext DbContext;
        public Repository(DbContext dbContext) => DbContext = dbContext;

        public async virtual Task<T> GetById(Guid id) => await DbContext.Set<T>().FindAsync(id);

        public async virtual Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                predicate = e => true;

            return await DbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task Add(params T[] entities)
        {
            DbContext.Set<T>().AddRange(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task Remove(params T[] entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            await DbContext.SaveChangesAsync();
        }
    }
}