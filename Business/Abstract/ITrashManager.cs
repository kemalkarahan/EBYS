using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITrashManager
    {
        Task InsertAsync(Trash entity);
        Task DeleteAsync(Trash entity);
        Task UpdateAsync(Trash entity);
        Task<bool> CheckAsync(int correspondenceId);
        Task<bool> CheckAsync(int correspondenceId, int staffId);
        Task<Trash> RetrieveAsync(int correspondenceId, int staffId);
        Task<List<Trash>> RetrieveAllAsync(int staffId);
    }
}
