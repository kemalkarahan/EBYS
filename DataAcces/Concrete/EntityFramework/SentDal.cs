using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class SentDal : EfEntityRepositoryBase<Sent, EfContext>, ISentDal
    {
        public SentDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
