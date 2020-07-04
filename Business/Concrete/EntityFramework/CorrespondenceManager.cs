using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class CorrespondenceManager : ICorrespondenceManager
    {
        private readonly ICorrespondenceDal correspondenceDal;

        public CorrespondenceManager(ICorrespondenceDal correspondenceDal)
        {
            this.correspondenceDal = correspondenceDal;
        }
        public async Task DeleteAsync(Correspondence entity)
        {
            await correspondenceDal.Delete(entity);
        }

        public async Task InsertAsync(Correspondence entity)
        {
            await correspondenceDal.Insert(entity);
        }

        public async Task<List<Correspondence>> RetrieveAllAsync(int id)
        {
            return await correspondenceDal.RetrieveAllWithAdditions(c => c.FromId == id);
        }

        public async Task<Correspondence> RetrieveAsync(int id)
        {
            return await correspondenceDal.RetrieveWithAdditions(c => c.Id == id);
        }

        public async Task<Correspondence> RetrieveLastAsync()
        {
            return await correspondenceDal.RetrieveLast();
        }

        public async Task UpdateAsync(Correspondence entity)
        {
            await correspondenceDal.Update(entity);
        }
    }
}
