using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class DraftManager : IDraftManager
    {
        private readonly IDraftDal draftDal;

        public DraftManager(IDraftDal draftDal)
        {
            this.draftDal = draftDal;
        }
        public async Task DeleteAsync(Draft entity)
        {
            await draftDal.Delete(entity);
        }

        public async Task InsertAsync(Draft entity)
        {
            await draftDal.Insert(entity);
        }

        public async Task<List<Draft>> RetrieveAllAsync(int id)
        {
            return await draftDal.RetrieveAll(d => d.StaffId == id);
        }

        public async Task<Draft> RetrieveAsync(int id)
        {
            return await draftDal.Retrieve(d => d.Id == id);
        }

        public async Task UpdateAsync(Draft entity)
        {
            await draftDal.Update(entity);
        }
    }
}
