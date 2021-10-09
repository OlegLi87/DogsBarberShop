using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DogsBarberShop.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DogsBarberShop.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> targetData;

        public GenericRepository(DbContext dbContext)
        {
            context = dbContext;
            targetData = context.Set<T>();
        }

        public async virtual Task<T> GetById(Guid id)
        {
            return await targetData.FindAsync(id);
        }

        public async virtual Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null)
                predicate = entity => true;

            return await targetData.Where(predicate).ToListAsync();
        }

        public virtual void Add(params T[] entities)
        {
            targetData.AddRange(entities);
        }

        public virtual void Delete(params T[] entities)
        {
            targetData.RemoveRange(entities);
        }

        public virtual async Task PatchUpdate(Guid id, Dictionary<string, dynamic> newValues)
        {
            var entityInDb = await targetData.FindAsync(id);
            if (entityInDb is null) return;

            var entityType = entityInDb.GetType();
            var props = entityType.GetProperties();

            foreach (var keyValue in newValues)
            {
                var prop = props.FirstOrDefault(p => p.Name != "Id" && p.Name == keyValue.Key);
                if (prop is not null)
                    prop.SetValue(entityInDb, keyValue.Value);
            }
        }

        public virtual void PutUpdate(T newEntityData)
        {
            targetData.Update(newEntityData);
        }
    }
}