using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DogsBarberShop_Api.Core.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null);
        Task Add(params T[] entities);
        Task Remove(params T[] entities);
    }
}