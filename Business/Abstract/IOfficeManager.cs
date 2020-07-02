using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOfficeManager
    {
        Task InsertAsync(Office entity);
        Task DeleteAsync(Office entity);
        Task UpdateAsync(Office entity);
        Task<int> RetrieveAsync(int unitId, string name);
        Task<List<Office>> RetrieveAllAsync(int unitId);
    }
}
