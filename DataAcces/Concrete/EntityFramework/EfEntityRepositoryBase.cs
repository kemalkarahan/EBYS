using DataAcces.Abstract;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAcces.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        protected readonly TContext context;

        public EfEntityRepositoryBase(TContext context)
        {
            this.context = context;
        }
        public async Task<bool> Check(Expression<Func<TEntity, bool>> filter)
        {
            return await context.Set<TEntity>().AnyAsync(filter);
        }

        public async Task Delete(TEntity entity)
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task Insert(TEntity entity)
        {
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public async Task<TEntity> Retrieve(Expression<Func<TEntity, bool>> filter)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> RetrieveAll(Expression<Func<TEntity, bool>> filter = null)
        {

            if(filter != null)
            {
                return await context.Set<TEntity>().Where(filter).ToListAsync();
            }
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
