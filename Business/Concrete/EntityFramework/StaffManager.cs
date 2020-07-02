using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class StaffManager : IStaffManager
    {
        private readonly IStaffDal staffDal;

        public StaffManager(IStaffDal staffDal)
        {
            this.staffDal = staffDal;
        }
        public async Task<bool> CheckAsync(string nickname)
        {
            return await staffDal.Check(s => s.Nickname == nickname);
        }

        public async Task DeleteAsync(Staff entity)
        {
            await staffDal.Delete(entity);
        }

        public async Task InsertAsync(Staff entity)
        {
            await staffDal.Insert(entity);
        }

        public async Task<Staff> RetrieveAsync(string identity)
        {
            if (int.TryParse(identity, out int id))
            {
                return await staffDal.Retrieve(s => s.Id == id);
            }
            return await staffDal.Retrieve(s => s.Nickname == identity);
        }

        public async Task<List<Staff>> RetrieveAllAsync()
        {
            return await staffDal.RetrieveAll(s => s.Id != 1);
        }

        public async Task<List<Staff>> RetrieveAllWithAdditionsAsync()
        {
            return await staffDal.RetrieveAllWithAdditions(s => s.Id != 1);
        }

        public async Task UpdateAsync(Staff entity)
        {
            await staffDal.Update(entity);
        }
    }
}
