using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAboutMeManager
    {
        Task InsertAsync(AboutMe entity);
        Task DeleteAsync(AboutMe entity);
        Task UpdateAsync(AboutMe entity);
        Task<AboutMe> RetrieveAsync(int id);
    }
}
