using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStaffDal : IRepository<Staff>
    {
        Task<List<Staff>> RetrieveAllWithAdditions(Expression<Func<Staff, bool>> filter);
    }
}
