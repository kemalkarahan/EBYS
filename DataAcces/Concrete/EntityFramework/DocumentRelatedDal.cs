using DataAcces.Abstract;
using Entities.Concrete;

namespace DataAcces.Concrete.EntityFramework
{
    public class DocumentRelatedDal : EfEntityRepositoryBase<DocumentRelated, EfContext>, IDocumentRelatedDal
    {
        public DocumentRelatedDal(EfContext efContext) : base(efContext)
        {

        }
    }
}
