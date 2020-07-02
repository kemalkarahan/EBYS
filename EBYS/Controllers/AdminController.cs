using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.NativeServices.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBYS.Controllers
{
    public class AdminController : Controller
    {
        private readonly IInstitutionManager institutionManager;
        private readonly IDutyManager dutyManager;
        private readonly IAboutMeManager aboutMeManager;
        private readonly ISystemService systemService;
        private readonly IMessageService messageService;
        private readonly IStaffManager staffManager;
        private readonly IInboxManager inboxManager;
        private readonly ISentManager sentManager;
        private readonly ITrashManager trashManager;
        private readonly IChairManager chairManager;
        private readonly IUnitManager unitManager;
        private readonly IOfficeManager officeManager;

        static int staffId;
        static int unitId;
        static List<int> remainingOfficeIds;
        static List<string> removedOfficeNames;
        static List<string> newOfficeNames;

        public AdminController(IInstitutionManager institutionManager,
                               IDutyManager dutyManager,
                               IAboutMeManager aboutMeManager,
                               IStaffManager staffManager,
                               IInboxManager inboxManager,
                               ISentManager sentManager,
                               ITrashManager trashManager,
                               IChairManager chairManager,
                               IUnitManager unitManager,
                               IOfficeManager officeManager,
                               ISystemService systemService,
                               IMessageService messageService)
        {
            this.institutionManager = institutionManager;
            this.dutyManager = dutyManager;
            this.aboutMeManager = aboutMeManager;
            this.institutionManager = institutionManager;
            this.systemService = systemService;
            this.messageService = messageService;
            this.staffManager = staffManager;
            this.inboxManager = inboxManager;
            this.sentManager = sentManager;
            this.trashManager = trashManager;
            this.chairManager = chairManager;
            this.staffManager = staffManager;
            this.unitManager = unitManager;
            this.officeManager = officeManager;
        }
        public async Task<IActionResult> Institution()
        {
            Institution ınstitution = await institutionManager.RetrieveAsync(1);
            ViewBag.Logo = ınstitution.Logo;
            InstitutionUpdateViewModel ınstitutionUpdate = new InstitutionUpdateViewModel
            {
                Name = ınstitution.Name,
                Domain = ınstitution.Domain,
                Email = ınstitution.Email,
                Password = ınstitution.Password,
                SmtpHost = ınstitution.SmtpHost.Substring(5),
                SmtpPort = ınstitution.SmtpPort
            };

            return View(ınstitutionUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Institution([Bind("Name, Domain, Logo, Email, Password, SmtpHost, SmtpPort")] InstitutionUpdateViewModel ınstitutionUpdate)
        {
            Institution institution = await institutionManager.RetrieveAsync(1);
            ViewBag.Logo = institution.Logo;
            if (ModelState.IsValid)
            {
                institution.Name = ınstitutionUpdate.Name;
                institution.Domain = ınstitutionUpdate.Domain;
                institution.Email = ınstitutionUpdate.Email;
                institution.Password = ınstitutionUpdate.Password;
                institution.SmtpHost = "smtp." + ınstitutionUpdate.SmtpHost.Trim();
                institution.SmtpPort = ınstitutionUpdate.SmtpPort;

                if (ınstitutionUpdate.Logo != null)
                {
                    await systemService.UploadImage(ınstitutionUpdate.Logo, "system", "Logo.png");
                    systemService.ImageResizing("Logo.png", "system");
                }

                await institutionManager.UpdateAsync(institution);
            }
            return View(ınstitutionUpdate);
        }

        public async Task<IActionResult> Staffs(string id)
        {
            staffId = 0;
            List<int> chairList = new List<int>();

            AddNewStaffViewModel viewModel = new AddNewStaffViewModel
            {
                OfficeList = new List<int>(),
                Staffs = await staffManager.RetrieveAllAsync(),
                Units = await unitManager.RetrieveAllWithAdditionsAsync(),
                DateOfBirth = DateTime.Now
            };

            foreach (Unit unit in viewModel.Units)
            {
                foreach (Office office in unit.Offices)
                {
                    if (!office.IsMultiple)
                    {
                        List<Chair> chairs = await chairManager.RetrieveAllWithOfficeInfoAsync(office.Id);
                        if (chairs != null && chairs.Count > 0)
                        {
                            chairList.Add(office.Id);
                        }
                    }
                }
            }

            viewModel.ChairList = chairList;

            if (!string.IsNullOrEmpty(id))
            {
                staffId = int.Parse(id);
                Staff staff = await staffManager.RetrieveAsync(id);
                List<Chair> chairs = await chairManager.RetrieveAllWithStaffInfoAsync(staff.Id);

                viewModel.Name = staff.Name;
                viewModel.LastName = staff.LastName;
                viewModel.Gender = staff.Gender;
                viewModel.DateOfBirth = staff.DateOfBirth;
                viewModel.Nickname = staff.Nickname;
                viewModel.Title = staff.Title;
                viewModel.UnitId = staff.UnitId;

                foreach (Chair chair in chairs)
                {
                    viewModel.OfficeList.Add(chair.OfficeId);
                    viewModel.ChairList.Remove(chair.OfficeId);
                }

                ViewBag.FormButtonLabel = "Güncelle";
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Staffs([Bind("Name, LastName, Nickname, Title, Gender, DateOfBirth, UnitId, OfficeList")] AddNewStaffViewModel viewModel)
        {
            List<Staff> staffs = await staffManager.RetrieveAllAsync();
            List<Unit> units = await unitManager.RetrieveAllWithAdditionsAsync();
            List<int> chairList = new List<int>();

            viewModel.Staffs = staffs;
            viewModel.Units = units;

            foreach (Unit unit in viewModel.Units)
            {
                foreach (Office office in unit.Offices)
                {
                    if (office.IsManager)
                    {
                        List<Chair> chairs = await chairManager.RetrieveAllWithOfficeInfoAsync(office.Id);
                        if (chairs != null && chairs.Count > 0)
                        {
                            chairList.Add(office.Id);
                        }
                    }
                }
            }

            viewModel.ChairList = chairList;

            if (ModelState.IsValid)
            {
                Staff staff;
                viewModel.Nickname = viewModel.Nickname.ToLower();

                if (staffId == 0)
                {
                    AboutMe aboutMe = new AboutMe
                    {
                        Notes = "Gösterilecek birşey mevcut değil!",
                        Skills = "Gösterilecek birşey mevcut değil!",
                        Education = "Gösterilecek birşey mevcut değil!"
                    };
                    staff = new Staff
                    {
                        Name = viewModel.Name.ToUpper(),
                        LastName = viewModel.LastName.ToUpper(),
                        DateOfBirth = viewModel.DateOfBirth,
                        Gender = viewModel.Gender.ToUpper(),
                        Title = viewModel.Title.ToUpper(),
                        UnitId = viewModel.UnitId,
                        Nickname = viewModel.Nickname.ToLower(),
                        ProfileImage = "DefaultProfileImage.png",
                        Password = await messageService.NewStaffAdded(viewModel.Nickname.ToLower(), viewModel.Name.ToUpper(), viewModel.LastName.ToUpper())
                    };

                    systemService.CreateFolder(viewModel.Nickname.ToLower());
                    await staffManager.InsertAsync(staff);
                    staff = await staffManager.RetrieveAsync(staff.Nickname.ToLower());

                    aboutMe.StaffId = staff.Id;
                    await aboutMeManager.InsertAsync(aboutMe);

                    if (viewModel.OfficeList != null)
                    {
                        foreach (int officeId in viewModel.OfficeList)
                        {
                            Chair tempChair = new Chair
                            {
                                OfficeId = officeId,
                                StaffId = staff.Id
                            };
                            await chairManager.InsertAsync(tempChair);
                        }
                    }
                }
                else
                {
                    staff = await staffManager.RetrieveAsync(viewModel.Nickname);
                    staff.Name = viewModel.Name;
                    staff.LastName = viewModel.LastName;
                    staff.DateOfBirth = viewModel.DateOfBirth;
                    staff.Gender = viewModel.Gender;
                    staff.Nickname = viewModel.Nickname;
                    staff.UnitId = viewModel.UnitId;
                    await staffManager.UpdateAsync(staff);

                    List<Chair> chairs = await chairManager.RetrieveAllWithStaffInfoAsync(staff.Id);

                    foreach (Chair chair in chairs)
                    {
                        await chairManager.DeleteAsync(chair);
                    }

                    if (viewModel.OfficeList != null)
                    {
                        foreach (int officeId in viewModel.OfficeList)
                        {
                            Chair tempChair = new Chair
                            {
                                OfficeId = officeId,
                                StaffId = staff.Id
                            };
                            await chairManager.InsertAsync(tempChair);
                        }
                    }
                }
                return RedirectToAction("Staffs", "Admin");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> ControlStaffExist(string nickname)
        {
            if (staffId != 0)
            {
                Staff staff = await staffManager.RetrieveAsync(staffId.ToString());
                if (staff.Nickname == nickname)
                    return Json(false);
            }

            return Json(await staffManager.CheckAsync(nickname));
        }

        [HttpPost]
        public async Task<JsonResult> StaffDelete(string id)
        {
            Staff staff = await staffManager.RetrieveAsync(id);
            if (staff == null)
            {
                ViewBag.ErrorMessage = "Böyle bir personel mevcut değil!";
                return Json(false);
            }
            else
            {
                try
                {
                    List<Duty> duties = await dutyManager.RetrieveAllAsync(staff.Id);
                    List<Chair> chairs = await chairManager.RetrieveAllWithStaffInfoAsync(staff.Id);
                    List<Inbox> inbox = await inboxManager.RetrieveAllWithStaffInfoAsync(staff.Id);
                    List<Sent> sent = await sentManager.RetrieveAllAsync(staff.Id);
                    List<Trash> trash = await trashManager.RetrieveAllAsync(staff.Id);

                    AboutMe aboutMe = await aboutMeManager.RetrieveAsync(staff.Id);

                    await aboutMeManager.DeleteAsync(aboutMe);

                    if (duties != null)
                        foreach (Duty duty in duties.ToList())
                            await dutyManager.DeleteAsync(duty);
                    if (chairs != null)
                        foreach (Chair chair in chairs.ToList())
                            await chairManager.DeleteAsync(chair);
                    if (inbox != null)
                        foreach (Inbox entity in inbox.ToList())
                            await inboxManager.DeleteAsync(entity);
                    if (sent != null)
                        foreach (Sent entity in sent.ToList())
                            await sentManager.DeleteAsync(entity);
                    if (inbox.Count >= 1)
                        foreach (Trash entity in trash.ToList())
                            await trashManager.DeleteAsync(entity);


                    systemService.DeleteFolder(staff.Nickname);
                    await staffManager.DeleteAsync(staff);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
            return Json(true);
        }

        public async Task<IActionResult> Units(int? id)
        {
            unitId = 0;
            remainingOfficeIds = new List<int>();
            removedOfficeNames = new List<string>();
            newOfficeNames = new List<string>();
            List<Unit> units = await unitManager.RetrieveAllWithAdditionsAsync();
            AddNewUnitViewModel viewModel = new AddNewUnitViewModel
            {
                Units = units
            };
            if (id != null)
            {
                unitId = id.GetValueOrDefault();
                Unit tempUnit = await unitManager.RetrieveWithAdditionsAsync((int)id);
                viewModel.Name = tempUnit.Name;
                viewModel.UpperUnitId = tempUnit.UpperUnitId;
                viewModel.Offices = tempUnit.Offices;
                viewModel.Manager = tempUnit.Offices.FirstOrDefault(o => o.IsManager == true).Name;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Units([Bind("Name, UpperUnitId, Manager, Offices")] AddNewUnitViewModel viewModel)
        {
            List<Unit> units = await unitManager.RetrieveAllWithAdditionsAsync();
            viewModel.Units = units;
            if (ModelState.IsValid)
            {
                Unit unit;
                if (unitId == 0)
                {
                    unit = new Unit
                    {
                        Name = viewModel.Name,
                        UpperUnitId = viewModel.UpperUnitId,
                    };

                    await unitManager.InsertAsync(unit);
                    unit = await unitManager.RetrieveLastAsync();

                    viewModel.Offices.Add(new Office
                    {
                        IsMultiple = false,
                        IsManager = true,
                        Name = viewModel.Manager
                    });

                    foreach (Office office in viewModel.Offices)
                    {
                        office.UnitId = unit.Id;
                        await officeManager.InsertAsync(office);
                    }
                }
                else
                {
                    unit = await unitManager.RetrieveWithAdditionsAsync(unitId);

                    List<Office> offices = unit.Offices;

                    foreach (Office office in offices.ToList())
                    {
                        if (office.IsManager)
                        {
                            office.Name = viewModel.Manager;
                            await officeManager.UpdateAsync(office);
                        }
                        if (remainingOfficeIds.Contains(office.Id))
                        {
                            int preOfficeIndex = remainingOfficeIds.IndexOf(office.Id);
                            Office tempOffice = viewModel.Offices.Find(o => o.Name == newOfficeNames[preOfficeIndex]);

                            office.Name = tempOffice.Name;
                            if (office.IsMultiple != tempOffice.IsMultiple)
                            {
                                office.IsMultiple = tempOffice.IsMultiple;
                                List<Chair> chairs = await chairManager.RetrieveAllWithOfficeInfoAsync(office.Id);

                                foreach (Chair chair in chairs.ToList())
                                    await chairManager.DeleteAsync(chair);
                            }
                            viewModel.Offices.Remove(tempOffice);

                            await officeManager.UpdateAsync(office);
                        }
                        else { if (!office.IsManager) { await officeManager.DeleteAsync(office); } }
                    }

                    foreach (Office office in viewModel.Offices)
                    {
                        if (!removedOfficeNames.Contains(office.Name))
                        {
                            office.UnitId = unit.Id;
                            await officeManager.InsertAsync(office);
                        }
                    }

                    unit.Name = viewModel.Name;
                    unit.UpperUnitId = viewModel.UpperUnitId;
                    await unitManager.UpdateAsync(unit);
                }
                return RedirectToAction("Units", "Admin");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> ControlUnitExist(string unitName)
        {
            if (unitId != 0)
            {
                Unit unit = await unitManager.RetrieveWithAdditionsAsync(unitId);
                if (unit.Name == unitName)
                    return Json(false);
            }

            return Json(await unitManager.CheckAsync(unitName));
        }

        [HttpPost]
        public async Task<JsonResult> UnitDelete(string id)
        {
            int unitId = int.Parse(id);

            try
            {
                Unit unit = await unitManager.RetrieveWithAdditionsAsync(unitId);
                List<Office> offices = await officeManager.RetrieveAllAsync(unitId);
                List<Unit> units = await unitManager.RetrieveAllWithAdditionsAsync();

                foreach (Office office in offices.ToList())
                {
                    List<Chair> chairs = await chairManager.RetrieveAllWithOfficeInfoAsync(office.Id);
                    if (chairs != null)
                        foreach (Chair chair in chairs.ToList())
                            await chairManager.DeleteAsync(chair);

                    await officeManager.DeleteAsync(office);
                }

                foreach (Unit upperUnitControl in units.ToList())
                {
                    if (upperUnitControl.UpperUnitId == unitId)
                    {
                        upperUnitControl.UpperUnitId = unit.UpperUnitId;
                        await unitManager.UpdateAsync(upperUnitControl);
                    }
                }

                await unitManager.DeleteAsync(unit);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Personel silinirken bir sorunla karşılaşıldı. Lütfen daha sonra tekrar deneyiniz.";
                return Json(false);
            }

            return Json(true);
        }

        [HttpPost]
        public JsonResult RemainingOffices(string id, string name)
        {
            id = id.Remove(id.IndexOf("_"), 3);
            remainingOfficeIds.Add(int.Parse(id));
            newOfficeNames.Add(name);
            return Json(null);
        }

        [HttpPost]
        public JsonResult RemovedOfficeNames(string name)
        {
            removedOfficeNames.Add(name);
            return Json(null);
        }
    }
}