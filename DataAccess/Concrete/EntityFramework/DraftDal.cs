using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class DraftDal : EfEntityRepositoryBase<Draft, EfContext>, IDraftDal
    {
        public DraftDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
