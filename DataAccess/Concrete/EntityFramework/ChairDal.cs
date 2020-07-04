using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class ChairDal : EfEntityRepositoryBase<Chair, EfContext>, IChairDal
    {
        public ChairDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
