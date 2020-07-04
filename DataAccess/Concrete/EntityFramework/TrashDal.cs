using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class TrashDal : EfEntityRepositoryBase<Trash, EfContext>, ITrashDal
    {
        public TrashDal(EfContext efContext):base(efContext)
        {

        }
    }
}
