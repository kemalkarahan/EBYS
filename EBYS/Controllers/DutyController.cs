using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Adapter.Abstract;
using Services.Adapter.Concrete;
using Services.NativeServices.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBYS.Controllers
{
    public class DutyController : Controller
    {
        private readonly ScheduledJobService scheduledJobService;
        private readonly IDutyManager dutyManager;
        private readonly IStaffManager staffManager;
        private readonly IMessageService messageService;
        private readonly IInstitutionManager institutionManager;

        public DutyController(IDutyManager dutyManager,
                              IStaffManager staffManager,
                              IMessageService messageService,
                              IInstitutionManager institutionManager)
        {
            scheduledJobService = new DelayedJobsAdapter(messageService);
            this.dutyManager = dutyManager;
            this.staffManager = staffManager;
            this.messageService = messageService;
            this.institutionManager = institutionManager;
        }
        public async Task<IActionResult> Charged()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            return View(await dutyManager.RetrieveAllAsync(staff.Id));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await dutyManager.RetrieveAsync(id));
        }

        public async Task<IActionResult> Charge()
        {
            ChargeTaskViewModel viewModel = new ChargeTaskViewModel();
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));

            List<Staff> staffs = await staffManager.RetrieveAllAsync();
            staffs.Remove(staff);

            viewModel.Staffs = staffs;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Charge([Bind("To,TaskName,TaskDescription,TaskType,Category,StartDate,EndDate")] ChargeTaskViewModel viewModel)
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));

            List<Staff> staffs = await staffManager.RetrieveAllAsync();
            staffs.Remove(staff);

            viewModel.Staffs = staffs;

            if (ModelState.IsValid)
            {
                Staff toInfo;
                Institution institution = await institutionManager.RetrieveAsync(1);
                foreach (int to in viewModel.To)
                {
                    toInfo = await staffManager.RetrieveAsync(to.ToString());

                    string jobId = scheduledJobService.Execute(new EmailViewModel
                    {
                        From = institution.Email,
                        Password = institution.Password,
                        Subject = "Görev Hatırlatması",
                        To = staff.Nickname,
                        Textarea = viewModel.EndDate.ToString() + " tarihinde bitirilmesi gereken " + viewModel.TaskName + " başlıklı göreviniz bulunmaktadır."
                    }, viewModel.EndDate);

                    await dutyManager.InsertAsync(new Duty
                    {
                        FromId = staff.Id,
                        TaskName = viewModel.TaskName,
                        TaskDescription = viewModel.TaskDescription,
                        TaskType = viewModel.TaskType,
                        Category = viewModel.Category,
                        StartDate = viewModel.StartDate,
                        EndDate = viewModel.EndDate,
                        To = to,
                        ReminderId = jobId
                    }
                    );

                    await messageService.Default(new EmailViewModel
                    {
                        From = institution.Email, //staff.Nickname
                        Password = institution.Password,//HttpContext.Session.GetString("userPassword")
                        Subject = "Görev Oluşturuldu Bilgisi",
                        To = toInfo.Nickname,
                        Textarea = "Sizden " + viewModel.TaskName + " başlıklı görevi tamamlamanız beklenmektedir."
                    });
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> CompleteDuty(int id)
        {
            Institution institution = await institutionManager.RetrieveAsync(1);
            Duty duty = await dutyManager.RetrieveAsync(id);

            await messageService.Default(new EmailViewModel
            {
                From = institution.Email, //HttpContext.Session.GetString("userNickname")
                Password = institution.Password, //HttpContext.Session.GetString("userPassword")
                Subject = "Görev Tamamlandı Bilgisi",
                To = duty.From.Nickname,
                Textarea = "Şahsıma tanımlamış olduğunuz " + duty.TaskName + " başlıklı görevin tamamlandığını bildiririm."
            });
            
            scheduledJobService.DeleteJob(duty.ReminderId);
            
            await dutyManager.DeleteAsync(duty);
            return Json(null);
        }

        [HttpPost]
        public async Task<JsonResult> CancelDuty(int id)
        {
            Institution institution = await institutionManager.RetrieveAsync(1);
            Duty duty = await dutyManager.RetrieveAsync(id);

            await messageService.Default(new EmailViewModel
            {
                From = institution.Email, //HttpContext.Session.GetString("userNickname")
                Password = institution.Password, //HttpContext.Session.GetString("userPassword")
                Subject = "Görev İptal Edildi Bilgisi",
                To = duty.From.Nickname,
                Textarea = "Şahsıma tanımlamış olduğunuz " + duty.TaskName + " başlıklı görevin iptal edildiğini bildiririm."
            });

            scheduledJobService.DeleteJob(duty.ReminderId);

            await dutyManager.DeleteAsync(duty);
            return Json(null);
        }
    }
}