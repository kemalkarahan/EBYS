using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class SubmissionManager : ISubmissionManager
    {
        private readonly ISubmissionDal submissionDal;

        public SubmissionManager(ISubmissionDal submissionDal)
        {
            this.submissionDal = submissionDal;
        }
        public async Task DeleteAsync(Submission entity)
        {
            await submissionDal.Delete(entity);
        }

        public async Task InsertAsync(Submission entity)
        {
            await submissionDal.Insert(entity);
        }

        public async Task<List<Submission>> RetrieveAllAsync(int id)
        {
            return await submissionDal.RetrieveAll(s => s.Id == id);
        }

        public async Task<Submission> RetrieveAsync(int id)
        {
            return await submissionDal.Retrieve(s => s.Id == id);
        }

        public async Task UpdateAsync(Submission entity)
        {
            await submissionDal.Update(entity);
        }
    }
}
