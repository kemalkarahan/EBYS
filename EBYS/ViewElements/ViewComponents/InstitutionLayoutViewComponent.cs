using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EBYS.ViewElements.ViewComponents
{
    public class InstitutionLayoutViewComponent : ViewComponent
    {
        private readonly IInstitutionManager institutionManager;

        public InstitutionLayoutViewComponent(IInstitutionManager db)
        {
            this.institutionManager = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Institution institution = await institutionManager.RetrieveAsync(1);
            return View(institution);
        }
    }
}
