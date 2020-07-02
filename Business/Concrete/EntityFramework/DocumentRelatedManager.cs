using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class DocumentRelatedManager : IDocumentRelatedManager
    {
        private readonly IDocumentRelatedDal documentRelatedDal;

        public DocumentRelatedManager(IDocumentRelatedDal documentRelatedDal)
        {
            this.documentRelatedDal = documentRelatedDal;
        }
        public async Task<bool> CheckAsync(int id)
        {
            return await documentRelatedDal.Check(r => r.Id == id);
        }
        public async Task DeleteAsync(DocumentRelated entity)
        {
            await documentRelatedDal.Delete(entity);
        }

        public async Task InsertAsync(DocumentRelated entity)
        {
            await documentRelatedDal.Insert(entity);
        }
    }
}
