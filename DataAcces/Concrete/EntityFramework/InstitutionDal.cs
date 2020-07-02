using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class InstitutionDal : EfEntityRepositoryBase<Institution, EfContext>, IInstitutionDal
    {
        public InstitutionDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
