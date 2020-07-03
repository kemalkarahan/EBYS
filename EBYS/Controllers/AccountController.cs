using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.NativeServices.Abstract;
using Utilities.Abstract;

namespace EBYS.Controllers
{
    [RequireHttps]
    public class AccountController : Controller
    {
        private readonly IUtility utilities;
        private readonly ISystemService systemService;
        private readonly IMessageService messageService;
        private readonly IStaffManager staffManager;
        private readonly IUnitManager unitManager;
        private readonly IOfficeManager officeManager;
        private readonly IAboutMeManager aboutMeManager;
        private readonly IChairManager chairManager;
        private readonly IInstitutionManager ınstitutionManager;
        private static string nickHashCode="null";

        public AccountController(IUtility utilities,
                                 ISystemService systemService,
                                 IMessageService messageService,
                                 IStaffManager staffManager,
                                 IUnitManager unitManager,
                                 IOfficeManager officeManager,
                                 IAboutMeManager aboutMeManager,
                                 IChairManager chairManager,
                                 IInstitutionManager ınstitutionManager)
        {
            this.utilities = utilities;
            this.systemService = systemService;
            this.messageService = messageService;
            this.staffManager = staffManager;
            this.unitManager = unitManager;
            this.officeManager = officeManager;
            this.aboutMeManager = aboutMeManager;
            this.chairManager = chairManager;
            this.ınstitutionManager = ınstitutionManager;
        }

        public async Task<IActionResult> Login()
        {
            Institution ınstitution = await ınstitutionManager.RetrieveAsync(1);
            ViewBag.InstitutionName = ınstitution.Name;
            ViewBag.Domain = ınstitution.Domain;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            Institution ınstitution = await ınstitutionManager.RetrieveAsync(1);
            ViewBag.InstitutionName = ınstitution.Name;
            ViewBag.Domain = ınstitution.Domain;
            if (ModelState.IsValid)
            {
                login.Nickname = login.Nickname.ToLower();
                bool IsStaffMatch = await systemService.LoginControl(login);
                if (IsStaffMatch)
                {
                    Staff admin = await staffManager.RetrieveAsync("1");
                    HttpContext.Session.SetString("userNickname", login.Nickname);
                    HttpContext.Session.SetString("userPassword", login.Password);
                    if (login.Nickname == admin.Nickname)
                    {
                        return RedirectToAction("Institution", "Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(login);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userNickname");
            return RedirectToAction("Login","Account");
        }
        public async Task<IActionResult> ForgetPassword()
        {
            Institution ınstitution = await ınstitutionManager.RetrieveAsync(1);
            ViewBag.InstitutionName = ınstitution.Name;
            ViewBag.Domain = ınstitution.Domain;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(string nickname)
        {
            Institution ınstitution = await ınstitutionManager.RetrieveAsync(1);
            ViewBag.InstitutionName = ınstitution.Name;
            ViewBag.Domain = ınstitution.Domain;
            if (!string.IsNullOrWhiteSpace(nickname))
            {
                nickname = nickname.ToLower();
                bool isExist = await staffManager.CheckAsync(nickname);
                if (isExist)
                {
                    Staff staff = await staffManager.RetrieveAsync(nickname);
                    await messageService.PasswordReset(staff);
                }
                else
                {
                    ViewBag.Error = "Eksik ya da hatalı veri girişi!";
                }
            }
            return View();
        }

        public IActionResult PasswordReset(string value)
        {
            nickHashCode = value;
            @ViewBag.BackToLoginPage = "none";
            @ViewBag.DisplayError = "none";
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> PasswordReset([Bind("NewPassword,NewPasswordAgain")]PasswordResetViewModel passwordReset)
        {
            @ViewBag.BackToLoginPage = "none";
            @ViewBag.DisplayError = "block";
            if (ModelState.IsValid)
            {
                List<Staff> controlList = await staffManager.RetrieveAllAsync();
                foreach (var staff in controlList)
                {
                    if (nickHashCode == utilities.ConvertToHashCode(staff.Nickname))
                    {
                        Staff tempStaff = await staffManager.RetrieveAsync(staff.Nickname);
                        tempStaff.Password = utilities.ConvertToHashCode(passwordReset.NewPassword);
                        await staffManager.UpdateAsync(tempStaff);
                        @ViewBag.BackToLoginPage = "block";
                        @ViewBag.DisplayError = "none";
                        break;
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            AboutMe aboutMe = await aboutMeManager.RetrieveAsync(staff.Id);
            ProfileEditViewModel profile = new ProfileEditViewModel
            {
                Staff = staff
            };
            profile.Staff.Unit = await unitManager.RetrieveWithAdditionsAsync(profile.Staff.UnitId);
            profile.Staff.Unit.Offices = await officeManager.RetrieveAllAsync(profile.Staff.UnitId);
            
            foreach (Office office in profile.Staff.Unit.Offices)
            {
                if (office.IsManager)
                {
                    List<Chair> chairs = await chairManager.RetrieveAllWithOfficeInfoAsync(office.Id);
                    if (chairs.Count == 1)
                    { 
                        profile.Manager = await staffManager.RetrieveAsync(chairs[0].StaffId.ToString());
                        profile.Manager.Unit = await unitManager.RetrieveWithAdditionsAsync(profile.Manager.UnitId);
                    }
                    break;
                }
            }

            profile.DateOfBirth = profile.Staff.DateOfBirth;
            profile.Gender = profile.Staff.Gender;
            profile.Education = aboutMe.Education;
            profile.Notes = aboutMe.Notes;
            profile.Skills = aboutMe.Skills;
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> Profile([Bind("ProfileImage, DateOfBirth, Gender, Education, Skills, Notes")]ProfileEditViewModel profile)
        {
            profile.Staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            profile.Staff.Unit = await unitManager.RetrieveWithAdditionsAsync(profile.Staff.UnitId);
            profile.Staff.Unit.Offices = await officeManager.RetrieveAllAsync(profile.Staff.UnitId);

            foreach (Office office in profile.Staff.Unit.Offices)
            {
                if (office.IsManager)
                {
                    List<Chair> chairs = await chairManager.RetrieveAllWithOfficeInfoAsync(office.Id);
                    if (chairs.Count == 1)
                    {
                        profile.Manager = await staffManager.RetrieveAsync(chairs[0].StaffId.ToString());
                        profile.Manager.Unit = await unitManager.RetrieveWithAdditionsAsync(profile.Manager.UnitId);
                    }
                    break;
                }
            }

            if (ModelState.IsValid)
            {
                Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
                AboutMe aboutMe = await aboutMeManager.RetrieveAsync(staff.Id);
                if (profile.ProfileImage != null)
                {
                    string imageName = staff.Nickname + Path.GetExtension(profile.ProfileImage.FileName);
                    await systemService.UploadImage(profile.ProfileImage, "user", imageName);
                    staff.ProfileImage = imageName;
                    systemService.ImageResizing(staff.ProfileImage, "user");
                }
                staff.DateOfBirth = profile.DateOfBirth;
                staff.Gender = profile.Gender;
                aboutMe.Education = profile.Education;
                aboutMe.Skills = profile.Skills;
                aboutMe.Notes = profile.Notes;

                await staffManager.UpdateAsync(staff);
                await aboutMeManager.UpdateAsync(aboutMe);

                return RedirectToAction("Profile");
            }
            return View(profile);
        }
    }
}