using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class SentManager : ISentManager
    {
        private readonly ISentDal sentDal;

        public SentManager(ISentDal sentDal)
        {
            this.sentDal = sentDal;
        }
        public async Task<bool> CheckAsync(int correspondenceId, int staffId)
        {
            return await sentDal.Check(s => s.CorrespondenceId == correspondenceId && s.StaffId == staffId);
        }

        public async Task DeleteAsync(Sent entity)
        {
            await sentDal.Delete(entity);
        }

        public async Task InsertAsync(Sent entity)
        {
            await sentDal.Insert(entity);
        }

        public async Task<List<Sent>> RetrieveAllAsync(int staffId)
        {
            return await sentDal.RetrieveAll(s => s.StaffId == staffId);
        }

        public async Task<Sent> RetrieveAsync(int correspondenceId, int staffId)
        {
            return await sentDal.Retrieve(s => s.CorrespondenceId == correspondenceId && s.StaffId == staffId);
        }

        public async Task UpdateAsync(Sent entity)
        {
            await sentDal.Update(entity);
        }
    }
}
