using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class InboxManager : IInboxManager
    {
        private readonly IInboxDal inboxDal;

        public InboxManager(IInboxDal inboxDal)
        {
            this.inboxDal = inboxDal;
        }
        public async Task<bool> CheckAsync(int correspondenceId, int staffId)
        {
            return await inboxDal.Check(i => i.CorrespondenceId == correspondenceId && i.StaffId == staffId);
        }

        public async Task DeleteAsync(Inbox entity)
        {
            await inboxDal.Delete(entity);
        }

        public async Task InsertAsync(Inbox entity)
        {
            await inboxDal.Insert(entity);
        }

        public async Task<List<Inbox>> RetrieveAllWithCorrespondenceInfoAsync(int id)
        {
            return await inboxDal.RetrieveAll(i => i.CorrespondenceId == id);
        }

        public async Task<List<Inbox>> RetrieveAllWithStaffInfoAsync(int id)
        {
            return await inboxDal.RetrieveAll(i => i.StaffId == id);
        }

        public async Task<Inbox> RetrieveAsync(int correspondenceId, int staffId)
        {
            return await inboxDal.Retrieve(i => i.CorrespondenceId == correspondenceId && i.StaffId == staffId);
        }

        public async Task UpdateAsync(Inbox entity)
        {
            await inboxDal.Update(entity);
        }
    }
}
