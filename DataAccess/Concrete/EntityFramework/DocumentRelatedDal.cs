using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class DocumentRelatedDal : EfEntityRepositoryBase<DocumentRelated, EfContext>, IDocumentRelatedDal
    {
        public DocumentRelatedDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
