using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class DocumentAttachmentDal : EfEntityRepositoryBase<DocumentAttachment, EfContext>, IDocumentAttachmentDal
    {
        public DocumentAttachmentDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
