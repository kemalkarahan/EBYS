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
    public class UnitDal : EfEntityRepositoryBase<Unit, EfContext>, IUnitDal
    {
        public UnitDal(EfContext efContext) : base(efContext)
        {

        }
        public async Task<List<Unit>> RetrieveAllWithAdditions()
        {
            return await context.Units.Where(u => u.Id != 1).Include(u => u.Offices).ToListAsync();
        }

        public async Task<List<Unit>> RetrieveAllWithUpperUnits()
        {
            return await context.Units.Where(u => u.Id != 1).Include(u => u.Offices).Include(u => u.UpperUnit).ToListAsync();
        }

        public async Task<Unit> RetrieveLast()
        {
            return await context.Units.Include(u => u.Offices).OrderByDescending(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<Unit> RetrieveWithAdditions(Expression<Func<Unit, bool>> filter)
        {
            return await context.Units.Include(u => u.Offices).FirstOrDefaultAsync(filter);
        }
    }
}
