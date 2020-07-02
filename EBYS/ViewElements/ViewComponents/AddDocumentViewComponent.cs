using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBYS.ViewElements.ViewComponents
{
    public class AddDocumentViewComponent : ViewComponent
    {
        private readonly IStaffManager staffManager;
        private readonly IDocumentManager documentManager;

        public AddDocumentViewComponent(IStaffManager db, IDocumentManager documentManager)
        {
            this.staffManager = db;
            this.documentManager = documentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            List<Document> documents = await documentManager.RetrieveAllAsync(staff.Id);
            return View(documents);
        }
    }
}
