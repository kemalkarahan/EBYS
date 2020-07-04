using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> Check(Expression<Func<T, bool>> filter);
        Task<T> Retrieve(Expression<Func<T, bool>> filter);
        Task<List<T>> RetrieveAll(Expression<Func<T, bool>> filter = null);
    }
}
