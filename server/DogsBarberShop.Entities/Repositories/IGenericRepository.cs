using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DogsBarberShop.Entities.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null);
        void Add(params T[] entites);
        void Delete(params T[] entities);
        Task PatchUpdate(Guid id, Dictionary<string, dynamic> newValues);
        void PutUpdate(T newEntityData);
    }
}