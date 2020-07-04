using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class DocumentDal : EfEntityRepositoryBase<Document, EfContext>, IDocumentDal
    {
        public DocumentDal(EfContext efContext) : base(efContext)
        {

        }
        public async Task<Document> RetrieveLast()
        {
            return await context.Documents.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
        }

        public async Task<Document> RetrieveWithAdditions(Expression<Func<Document, bool>> filter)
        {
            return await context.Documents.Include(d => d.DocumentRelateds).Include(d => d.DocumentAttachments).FirstOrDefaultAsync(filter);
        }
    }
}
