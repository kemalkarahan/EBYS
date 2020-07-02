using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IChairManager
    {
        Task InsertAsync(Chair entity);
        Task DeleteAsync(Chair entity);
        Task UpdateAsync(Chair entity);
        Task<List<Chair>> RetrieveAllWithStaffInfoAsync(int id);
        Task<List<Chair>> RetrieveAllWithOfficeInfoAsync(int id);
    }
}
