using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISubmissionManager
    {
        Task InsertAsync(Submission entity);
        Task DeleteAsync(Submission entity);
        Task UpdateAsync(Submission entity);
        Task<Submission> RetrieveAsync(int id);
        Task<List<Submission>> RetrieveAllAsync(int id);
    }
}
