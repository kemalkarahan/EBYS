using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStaffManager
    {
        Task InsertAsync(Staff entity);
        Task UpdateAsync(Staff entity);
        Task DeleteAsync(Staff entity);
        Task<bool> CheckAsync(string nickname);
        Task<Staff> RetrieveAsync(string identity);
        Task<List<Staff>> RetrieveAllAsync();
        Task<List<Staff>> RetrieveAllWithAdditionsAsync();
    }
}
