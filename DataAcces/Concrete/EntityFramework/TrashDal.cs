using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class TrashDal : EfEntityRepositoryBase<Trash, EfContext>, ITrashDal
    {
        public TrashDal(EfContext efContext):base(efContext)
        {

        }
    }
}
