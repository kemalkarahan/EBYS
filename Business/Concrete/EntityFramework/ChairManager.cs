using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class ChairManager : IChairManager
    {
        private readonly IChairDal chairDal;

        public ChairManager(IChairDal chairDal)
        {
            this.chairDal = chairDal;
        }
        public async Task DeleteAsync(Chair entity)
        {
            await chairDal.Delete(entity);
        }

        public async Task InsertAsync(Chair entity)
        {
            await chairDal.Insert(entity);
        }

        public async Task<List<Chair>> RetrieveAllWithOfficeInfoAsync(int id)
        {
            return await chairDal.RetrieveAll(c => c.OfficeId == id);
        }

        public async Task<List<Chair>> RetrieveAllWithStaffInfoAsync(int id)
        {
            return await chairDal.RetrieveAll(c => c.StaffId == id);
        }

        public async Task UpdateAsync(Chair entity)
        {
            await chairDal.Update(entity);
        }
    }
}
