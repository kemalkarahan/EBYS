using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class StaffDal : EfEntityRepositoryBase<Staff, EfContext>, IStaffDal
    {
        public StaffDal(EfContext efContext) : base(efContext)
        {

        }
        public async Task<List<Staff>> RetrieveAllWithAdditions(Expression<Func<Staff, bool>> filter)
        {
            return await context.Staffs.Where(filter).Include(d => d.Chairs).ToListAsync();
        }
    }
}
