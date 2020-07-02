using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDocumentAttachmentManager
    {
        Task<bool> CheckAsync(int id);
        Task InsertAsync(DocumentAttachment entity);
        Task DeleteAsync(DocumentAttachment entity);
    }
}
