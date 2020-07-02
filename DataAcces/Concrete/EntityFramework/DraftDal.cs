using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class DraftDal : EfEntityRepositoryBase<Draft, EfContext>, IDraftDal
    {
        public DraftDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
