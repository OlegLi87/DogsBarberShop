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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected DbContext Context;
        protected DbSet<T> TargetData;

        public GenericRepository(DbContext dbContext)
        {
            Context = dbContext;
            TargetData = Context.Set<T>();
        }

        public async virtual Task<T> GetById(Guid id)
        {
            return await TargetData.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async virtual Task<List<T>> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate is null)
                predicate = entity => true;

            return await TargetData.Where(predicate).AsNoTracking().ToListAsync();
        }

        public async virtual Task Add(params T[] entities)
        {
            TargetData.AddRange(entities);
            await Context.SaveChangesAsync();
        }

        public async virtual Task Delete(params T[] entities)
        {
            TargetData.RemoveRange(entities);
            await Context.SaveChangesAsync();
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

            await Context.SaveChangesAsync();
        }

        public async virtual Task PutUpdate(T newEntityData)
        {
            TargetData.Update(newEntityData);
            await Context.SaveChangesAsync();
        }
    }
}