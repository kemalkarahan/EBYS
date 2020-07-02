using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EBYS.ViewElements.ViewComponents
{
    public class NavBarControlViewComponent : ViewComponent
    {
        private readonly IStaffManager staffManager;

        public NavBarControlViewComponent(IStaffManager staffManager)
        {
            this.staffManager = staffManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            if (staff.Nickname == "super.admin")
            {
                return View("AdminNavBar");
            }
            return View();
        }
    }
}
