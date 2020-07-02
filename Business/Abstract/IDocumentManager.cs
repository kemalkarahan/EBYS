using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDocumentManager
    {
        Task InsertAsync(Document entity);
        Task UpdateAsync(Document entity);
        Task DeleteAsync(Document entity);
        Task<bool> CheckAsync(int id);
        Task<Document> RetrieveAsync(int id);
        Task<Document> RetrieveWithAdditionsAsync(int id);
        Task<List<Document>> RetrieveAllAsync(int id);
        Task<List<Document>> PrepareDocumentsAsync(List<IFormFile> files, Staff staff);
        Task<string> SaveAsync(IFormFile file, string folder, string directoryFolder = "user");
        void DeleteDocument(Document document, string folder, string directoryFolder = "user");
        Task ShareDocumentAsync(List<string> nicknameLİst, int documentId, string staffNickname);
        Task<Document> RetrieveLastAsync();
    }
}
