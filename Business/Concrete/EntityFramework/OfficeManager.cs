using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class OfficeManager : IOfficeManager
    {
        private readonly IOfficeDal officeDal;

        public OfficeManager(IOfficeDal officeDal)
        {
            this.officeDal = officeDal;
        }
        public async Task DeleteAsync(Office entity)
        {
            await officeDal.Delete(entity);
        }

        public async Task InsertAsync(Office entity)
        {
            await officeDal.Insert(entity);
        }

        public async Task<List<Office>> RetrieveAllAsync(int unitId)
        {
            return await officeDal.RetrieveAll(o => o.UnitId == unitId);
        }

        public async Task<int> RetrieveAsync(int unitId, string name)
        {
            return await officeDal.RetrieveId(o => o.UnitId == unitId && o.Name == name);
        }

        public async Task UpdateAsync(Office entity)
        {
            await officeDal.Update(entity);
        }
    }
}
