using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IInboxManager
    {
        Task InsertAsync(Inbox entity);
        Task DeleteAsync(Inbox entity);
        Task UpdateAsync(Inbox entity);
        Task<bool> CheckAsync(int correspondenceId, int staffId);
        Task<Inbox> RetrieveAsync(int correspondenceId, int staffId);
        Task<List<Inbox>> RetrieveAllWithStaffInfoAsync(int id);
        Task<List<Inbox>> RetrieveAllWithCorrespondenceInfoAsync(int id);
    }
}
