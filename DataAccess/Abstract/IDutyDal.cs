using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDutyDal : IRepository<Duty>
    {
        Task<Duty> RetrieveWithAdditions(Expression<Func<Duty, bool>> filter);
        Task<List<Duty>> RetrieveAllWithAdditions(Expression<Func<Duty, bool>> filter);
    }
}
