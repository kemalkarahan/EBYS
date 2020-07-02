using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class DocumentAttachmentManager : IDocumentAttachmentManager
    {
        private readonly IDocumentAttachmentDal documentAttachmentDal;

        public DocumentAttachmentManager(IDocumentAttachmentDal documentAttachmentDal)
        {
            this.documentAttachmentDal = documentAttachmentDal;
        }

        public async Task<bool> CheckAsync(int id)
        {
            return await documentAttachmentDal.Check(a => a.Id == id);
        }
        public async Task DeleteAsync(DocumentAttachment entity)
        {
            await documentAttachmentDal.Delete(entity);
        }

        public async Task InsertAsync(DocumentAttachment entity)
        {
            await documentAttachmentDal.Insert(entity);
        }
    }
}
