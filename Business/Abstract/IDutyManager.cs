using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDutyManager
    {
        Task InsertAsync(Duty entity);
        Task DeleteAsync(Duty entity);
        Task UpdateAsync(Duty entity);
        Task<Duty> RetrieveAsync(int id);
        Task<List<Duty>> RetrieveAllAsync(int id);
    }
}
