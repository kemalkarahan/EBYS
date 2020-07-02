using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class ChairDal : EfEntityRepositoryBase<Chair, EfContext>, IChairDal
    {
        public ChairDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
