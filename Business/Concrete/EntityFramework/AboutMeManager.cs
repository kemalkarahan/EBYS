using Business.Abstract;
using DataAcces.Abstract;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class AboutMeManager : IAboutMeManager
    {
        private readonly IAboutMeDal aboutMeDal;

        public AboutMeManager(IAboutMeDal aboutMeDal)
        {
            this.aboutMeDal = aboutMeDal;
        }
        public async Task DeleteAsync(AboutMe entity)
        {
            await aboutMeDal.Delete(entity);
        }

        public async Task InsertAsync(AboutMe entity)
        {
            await aboutMeDal.Insert(entity);
        }

        public async Task<AboutMe> RetrieveAsync(int id)
        {
            return await aboutMeDal.Retrieve(a => a.StaffId == id);
        }

        public async Task UpdateAsync(AboutMe entity)
        {
            await aboutMeDal.Update(entity);
        }
    }
}
