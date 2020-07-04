using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class InstitutionDal : EfEntityRepositoryBase<Institution, EfContext>, IInstitutionDal
    {
        public InstitutionDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
