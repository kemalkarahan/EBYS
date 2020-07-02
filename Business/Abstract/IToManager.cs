using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IToManager
    {
        Task InsertAsync(To entity);
        Task DeleteAsync(To entity);
        Task UpdateAsync(To entity);
    }
}
