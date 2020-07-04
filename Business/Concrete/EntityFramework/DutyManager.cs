using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class DutyManager : IDutyManager
    {
        private readonly IDutyDal dutyDal;

        public DutyManager(IDutyDal dutyDal)
        {
            this.dutyDal = dutyDal;
        }
        public async Task DeleteAsync(Duty entity)
        {
            await dutyDal.Delete(entity);
        }

        public async Task InsertAsync(Duty entity)
        {
            await dutyDal.Insert(entity);
        }

        public async Task<List<Duty>> RetrieveAllAsync(int id)
        {
            return await dutyDal.RetrieveAllWithAdditions(d => d.To == id);
        }

        public async Task<Duty> RetrieveAsync(int id)
        {
            return await dutyDal.RetrieveWithAdditions(d => d.Id == id);
        }

        public async Task UpdateAsync(Duty entity)
        {
            await dutyDal.Update(entity);
        }
    }
}
