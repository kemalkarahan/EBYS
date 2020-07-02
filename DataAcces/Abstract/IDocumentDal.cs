using Entities.Concrete;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAcces.Abstract
{
    public interface IDocumentDal : IRepository<Document>
    {
        Task<Document> RetrieveWithAdditions(Expression<Func<Document, bool>> filter);
        Task<Document> RetrieveLast();
    }
}
