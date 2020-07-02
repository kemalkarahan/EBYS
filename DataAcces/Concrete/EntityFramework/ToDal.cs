using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class ToDal : EfEntityRepositoryBase<To, EfContext>, IToDal
    {
        public ToDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
