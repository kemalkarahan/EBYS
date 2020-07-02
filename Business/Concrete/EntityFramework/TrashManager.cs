using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class TrashManager : ITrashManager
    {
        private readonly ITrashDal trashDal;

        public TrashManager(ITrashDal trashDal)
        {
            this.trashDal = trashDal;
        }
        public async Task<bool> CheckAsync(int correspondenceId)
        {
            return await trashDal.Check(t => t.CorrespondenceId == correspondenceId);
        }

        public async Task<bool> CheckAsync(int correspondenceId, int staffId)
        {
            return await trashDal.Check(t => t.CorrespondenceId == correspondenceId && t.StaffId == staffId);
        }

        public async Task DeleteAsync(Trash entity)
        {
            await trashDal.Delete(entity);
        }

        public async Task InsertAsync(Trash entity)
        {
            await trashDal.Insert(entity);
        }

        public async Task<List<Trash>> RetrieveAllAsync(int staffId)
        {
            return await trashDal.RetrieveAll(t => t.StaffId == staffId);
        }

        public async Task<Trash> RetrieveAsync(int correspondenceId, int staffId)
        {
            return await trashDal.Retrieve(t => t.CorrespondenceId == correspondenceId && t.StaffId == staffId);
        }

        public async Task UpdateAsync(Trash entity)
        {
            await trashDal.Update(entity);
        }
    }
}
