using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class ToManager : IToManager
    {
        private readonly IToDal toDal;

        public ToManager(IToDal toDal)
        {
            this.toDal = toDal;
        }
        public async Task DeleteAsync(To entity)
        {
            await toDal.Delete(entity);
        }

        public async Task InsertAsync(To entity)
        {
            await toDal.Insert(entity);
        }

        public async Task UpdateAsync(To entity)
        {
            await toDal.Update(entity);
        }
    }
}
