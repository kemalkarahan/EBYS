using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class SentDal : EfEntityRepositoryBase<Sent, EfContext>, ISentDal
    {
        public SentDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
