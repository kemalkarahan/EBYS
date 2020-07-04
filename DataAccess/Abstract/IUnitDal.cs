using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUnitDal : IRepository<Unit>
    {
        Task<Unit> RetrieveLast();
        Task<Unit> RetrieveWithAdditions(Expression<Func<Unit, bool>> filter);
        Task<List<Unit>> RetrieveAllWithAdditions();
        Task<List<Unit>> RetrieveAllWithUpperUnits();
    }
}
