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

        public async virtual Task<List<T>> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null)
                predicate = entity => true;

            return await targetData.Where(predicate).ToListAsync();
        }

        public async virtual Task Add(params T[] entities)
        {
            targetData.AddRange(entities);
            await context.SaveChangesAsync();
        }

        public async virtual Task Delete(params T[] entities)
        {
            targetData.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public virtual async Task PatchUpdate(T entity, Dictionary<string, dynamic> newValues)
        {
            var entityType = entity.GetType();
            var props = entityType.GetProperties();

            foreach (var keyValue in newValues)
            {
                var prop = props.FirstOrDefault(p => p.Name != "Id" && p.Name == keyValue.Key);
                if (prop is not null)
                    prop.SetValue(entity, keyValue.Value);
            }

            await context.SaveChangesAsync();
        }

        public async virtual Task PutUpdate(T newEntityData)
        {
            targetData.Update(newEntityData);
            await context.SaveChangesAsync();
        }
    }
}