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
    public class CorrespondenceDal : EfEntityRepositoryBase<Correspondence, EfContext>, ICorrespondenceDal
    {
        public CorrespondenceDal(EfContext efContext) : base(efContext)
        {

        }
        public async Task<Correspondence> RetrieveLast()
        {
            return await context.Correspondences.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
        }

        public async Task<List<Correspondence>> RetrieveAllWithAdditions(Expression<Func<Correspondence, bool>> filter)
        {
            return await context.Correspondences.Where(filter)
                        .Include(c => c.From)
                        .Include(c => c.Tos)
                        .OrderByDescending(c => c.SentAt)
                        .ToListAsync();
        }

        public async Task<Correspondence> RetrieveWithAdditions(Expression<Func<Correspondence, bool>> filter)
        {
            return await context.Correspondences.Include(c => c.From).Include(c => c.Tos).FirstOrDefaultAsync(filter);
        }
    }
}
