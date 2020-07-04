using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class InboxDal : EfEntityRepositoryBase<Inbox, EfContext>, IInboxDal
    {
        public InboxDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
