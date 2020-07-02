using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IInstitutionManager
    {
        Task<Institution> RetrieveAsync(int id);
        Task UpdateAsync(Institution institution);
    }
}
