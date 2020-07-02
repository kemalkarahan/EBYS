using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDocumentRelatedManager
    {
        Task<bool> CheckAsync(int id);
        Task InsertAsync(DocumentRelated entity);
        Task DeleteAsync(DocumentRelated entity);
    }
}
