using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUnitManager
    {
        Task<bool> CheckAsync(string unitName);
        Task InsertAsync(Unit entity);
        Task DeleteAsync(Unit entity);
        Task UpdateAsync(Unit entity);
        Task<Unit> RetrieveLastAsync();
        Task<Unit> RetrieveWithAdditionsAsync(int id);
        Task<Unit> RetrieveWithAdditionsAsync(string name);
        Task<List<Unit>> RetrieveAllWithAdditionsAsync();
        Task<List<Unit>> RetrieveAllWithUpperUnitsAsync();
    }
}
