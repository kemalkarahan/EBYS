using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class ToDal : EfEntityRepositoryBase<To, EfContext>, IToDal
    {
        public ToDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
