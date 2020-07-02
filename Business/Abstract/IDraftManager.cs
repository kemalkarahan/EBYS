using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDraftManager
    {
        Task InsertAsync(Draft entity);
        Task DeleteAsync(Draft entity);
        Task UpdateAsync(Draft entity);
        Task<Draft> RetrieveAsync(int id);
        Task<List<Draft>> RetrieveAllAsync(int id);
    }
}
