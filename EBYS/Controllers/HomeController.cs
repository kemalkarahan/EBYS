using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;

namespace EBYS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDutyManager dutyManager;
        private readonly IDocumentManager documentManager;
        private readonly IStaffManager staffManager;

        public HomeController(IDutyManager dutyManager, IDocumentManager documentManager, IStaffManager staffManager)
        {
            this.dutyManager = dutyManager;
            this.documentManager = documentManager;
            this.staffManager = staffManager;
        }

        public async Task<IActionResult> Index()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            List<Duty> duties = await dutyManager.RetrieveAllAsync(staff.Id);
            List<Document> documents = await documentManager.RetrieveAllAsync(staff.Id);

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Documents = documents.OrderByDescending(d => d.Id).Take(5).ToList(),
                IndexTaskInfos = new List<IndexTaskInfo>()
            };

            foreach (Duty duty in duties.OrderByDescending(t => t.Id).Take(10).ToList())
            {
                IndexTaskInfo taskInfo = new IndexTaskInfo
                {
                    TaskName = duty.TaskName,
                    TaskType = duty.TaskType,
                    Category = duty.Category,
                    Datetime = duty.StartDate.ToString("dd/MM/yyyy") +"-"+ duty.EndDate.ToString("dd/MM/yyyy")
                };
                
                viewModel.IndexTaskInfos.Add(taskInfo);
            }

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(/*new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }*/);
        }
    }
}
