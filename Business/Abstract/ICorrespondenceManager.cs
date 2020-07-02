using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICorrespondenceManager
    {
        Task InsertAsync(Correspondence entity);
        Task DeleteAsync(Correspondence entity);
        Task UpdateAsync(Correspondence entity);
        Task<Correspondence> RetrieveLastAsync();
        Task<Correspondence> RetrieveAsync(int id);
        Task<List<Correspondence>> RetrieveAllAsync(int id);
    }
}
