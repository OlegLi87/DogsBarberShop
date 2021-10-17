using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DogsBarberShop.Entities.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<List<T>> Get(Expression<Func<T, bool>> predicate = null);
        Task Add(params T[] entites);
        Task Delete(params T[] ids);
        Task PatchUpdate(T id, Dictionary<string, dynamic> newValues);
        Task PutUpdate(T newEntityData);
    }
}