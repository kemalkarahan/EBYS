using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISentManager
    {
        Task InsertAsync(Sent entity);
        Task DeleteAsync(Sent entity);
        Task UpdateAsync(Sent entity);
        Task<bool> CheckAsync(int correspondenceId, int staffId);
        Task<Sent> RetrieveAsync(int correspondenceId, int staffId);
        Task<List<Sent>> RetrieveAllAsync(int staffId);
    }
}
