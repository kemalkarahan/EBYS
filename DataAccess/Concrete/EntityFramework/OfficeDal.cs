using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class OfficeDal : EfEntityRepositoryBase<Office, EfContext>, IOfficeDal
    {
        public OfficeDal(EfContext efContext) : base(efContext)
        {

        }
        public async Task<int> RetrieveId(Expression<Func<Office, bool>> filter)
        {
            Office office = await context.Set<Office>().FirstOrDefaultAsync(filter);
            return office.Id;
        }
    }
}
