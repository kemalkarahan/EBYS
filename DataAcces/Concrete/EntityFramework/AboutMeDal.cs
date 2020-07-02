using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class AboutMeDal : EfEntityRepositoryBase<AboutMe, EfContext>, IAboutMeDal
    {
        public AboutMeDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
