using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EBYS.ViewElements.ViewComponents
{
    public class MenuControlViewComponent : ViewComponent
    {
        private readonly IStaffManager staffManager;

        public MenuControlViewComponent(IStaffManager staffManager)
        {
            this.staffManager = staffManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            if (staff.Nickname == "super.admin")
            {
                return View("AdminMenu");
            }

            return View();
        }
    }
}
