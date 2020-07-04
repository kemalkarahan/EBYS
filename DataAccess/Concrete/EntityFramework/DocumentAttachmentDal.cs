using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class DocumentAttachmentDal : EfEntityRepositoryBase<DocumentAttachment, EfContext>, IDocumentAttachmentDal
    {
        public DocumentAttachmentDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
