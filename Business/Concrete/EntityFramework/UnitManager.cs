using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class UnitManager : IUnitManager
    {
        private readonly IUnitDal unitDal;

        public UnitManager(IUnitDal unitDal)
        {
            this.unitDal = unitDal;
        }

        public async Task<bool> CheckAsync(string unitName)
        {
            return await unitDal.Check(u => u.Name == unitName);
        }
        public async Task DeleteAsync(Unit entity)
        {
            await unitDal.Delete(entity);
        }

        public async Task InsertAsync(Unit entity)
        {
            await unitDal.Insert(entity);
        }

        public async Task<List<Unit>> RetrieveAllWithAdditionsAsync()
        {
            return await unitDal.RetrieveAllWithAdditions();
        }

        public async Task<List<Unit>> RetrieveAllWithUpperUnitsAsync()
        {
            return await unitDal.RetrieveAllWithUpperUnits();
        }

        public async Task<Unit> RetrieveWithAdditionsAsync(int id)
        {
            return await unitDal.RetrieveWithAdditions(u => u.Id == id);
        }

        public async Task<Unit> RetrieveWithAdditionsAsync(string name)
        {
            return await unitDal.RetrieveWithAdditions(u => u.Name == name);
        }

        public async Task<Unit> RetrieveLastAsync()
        {
            return await unitDal.RetrieveLast();
        }

        public async Task UpdateAsync(Unit entity)
        {
            await unitDal.Update(entity);
        }
    }
}
