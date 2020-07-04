using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICorrespondenceDal : IRepository<Correspondence>
    {
        Task<Correspondence> RetrieveLast();
        Task<Correspondence> RetrieveWithAdditions(Expression<Func<Correspondence, bool>> filter);
        Task<List<Correspondence>> RetrieveAllWithAdditions(Expression<Func<Correspondence, bool>> filter);
    }
}
