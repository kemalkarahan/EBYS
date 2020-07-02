using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class InboxDal : EfEntityRepositoryBase<Inbox, EfContext>, IInboxDal
    {
        public InboxDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
