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
    public class DutyDal : EfEntityRepositoryBase<Duty, EfContext>, IDutyDal
    {
        public DutyDal(EfContext efContext) : base(efContext)
        {

        }
        public async Task<List<Duty>> RetrieveAllWithAdditions(Expression<Func<Duty,bool>> filter)
        {
            return await context.Set<Duty>().Where(filter).Include(d => d.From).ToListAsync();
        }

        public async Task<Duty> RetrieveWithAdditions(Expression<Func<Duty, bool>> filter)
        {
            return await context.Set<Duty>().Include(d => d.From).FirstOrDefaultAsync(filter);
        }
    }
}
