using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class AboutMeDal : EfEntityRepositoryBase<AboutMe, EfContext>, IAboutMeDal
    {
        public AboutMeDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
