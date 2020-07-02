using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EBYS.ViewElements.ViewComponents
{
    public class SidebarUserViewComponent : ViewComponent
    {
        private readonly IStaffManager staffManager;

        public SidebarUserViewComponent(IStaffManager staffManager)
        {
            this.staffManager = staffManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            return View(staff);
        }
    }
}
