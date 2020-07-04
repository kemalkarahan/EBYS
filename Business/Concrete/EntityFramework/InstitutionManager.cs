using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class InstitutionManager : IInstitutionManager
    {
        private readonly IInstitutionDal institutionDal;

        public InstitutionManager(IInstitutionDal institutionDal)
        {
            this.institutionDal = institutionDal;
        }
        public async Task<Institution> RetrieveAsync(int id)
        {
           return await institutionDal.Retrieve(i => i.Id == id);
        }

        public async Task UpdateAsync(Institution institution)
        {
            await institutionDal.Update(institution);
        }
    }
}
