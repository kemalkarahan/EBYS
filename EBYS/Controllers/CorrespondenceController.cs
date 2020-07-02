using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.NativeServices.Abstract;

namespace EBYS.Controllers
{
    public class CorrespondenceController : Controller
    {
        private readonly IInstitutionManager ınstitutionManager;
        private readonly IStaffManager staffManager;
        private readonly IDraftManager draftManager;
        private readonly IMessageService messageService;
        private readonly ICorrespondenceManager correspondenceManager;
        private readonly IToManager toManager;
        private readonly IInboxManager inboxManager;
        private readonly ISentManager sentManager;
        private readonly ITrashManager trashManager;
        private readonly IDocumentManager documentManager;
        private readonly IWebHostEnvironment hostEnvironment;
        Staff staff;
        Draft draft;
        Correspondence correspondence;

        public CorrespondenceController(IInstitutionManager ınstitutionManager,
                                        IStaffManager staffManager,
                                        IDraftManager draftManager,
                                        IMessageService messageService,
                                        ICorrespondenceManager correspondenceManager,
                                        IToManager toManager,
                                        IInboxManager inboxManager,
                                        ISentManager sentManager,
                                        ITrashManager trashManager,
                                        IDocumentManager documentManager,
                                        IWebHostEnvironment hostEnvironment)
        {
            this.ınstitutionManager = ınstitutionManager;
            this.staffManager = staffManager;
            this.draftManager = draftManager;
            this.messageService = messageService;
            this.correspondenceManager = correspondenceManager;
            this.toManager = toManager;
            this.inboxManager = inboxManager;
            this.sentManager = sentManager;
            this.trashManager = trashManager;
            this.documentManager = documentManager;
            this.hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Compose(int? id)
        {
            ViewBag.IsCompose = true;
            
            draft = new Draft();
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            
            List<Staff> staffs = await staffManager.RetrieveAllAsync();
            staffs.Remove(staff);
            
            if (id.HasValue)
                draft = await draftManager.RetrieveAsync(id.GetValueOrDefault());
            
            return View(new CorrespondenceViewModel
            {
                Staffs = staffs,
                Context = draft.Context,
                Title = draft.Title
            });
        }
        [HttpPost]
        public async Task<IActionResult> ReplyOrForward(int id, bool IsReply)
        {
            Correspondence correspondence = await correspondenceManager.RetrieveAsync(id);
            CorrespondenceViewModel viewModel = new CorrespondenceViewModel
            {
                Context = correspondence.Context
            };
            if (IsReply)
            {
                viewModel.Title = "Re: " + correspondence.Title;
                viewModel.ToList.Add(correspondence.FromId);
            }
            else
                viewModel.Title = "Fwd: " + correspondence.Title;
            return View("Compose", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Compose([Bind("ToList, Title, Context")] CorrespondenceViewModel viewModel)
        {
            ViewBag.IsCompose = true;
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            
            viewModel.Staffs = await staffManager.RetrieveAllAsync();
            viewModel.Staffs.Remove(staff);
            
            if (ModelState.IsValid)
            {
                
                Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));

                correspondence = new Correspondence
                {
                    FromId = staff.Id,
                    SentAt = DateTime.Now,
                    Context = viewModel.Context,
                    Title = viewModel.Title
                };
                await correspondenceManager.InsertAsync(correspondence);
                correspondence = await correspondenceManager.RetrieveLastAsync();

                foreach (int toId in viewModel.ToList)
                {
                    await inboxManager.InsertAsync(new Inbox
                    {
                        CorrespondenceId = correspondence.Id,
                        StaffId = toId
                    });

                    await toManager.InsertAsync(new To
                    {
                        CorrespondenceId = correspondence.Id,
                        StaffId = toId
                    });
                }

                await sentManager.InsertAsync(new Sent
                {
                    CorrespondenceId = correspondence.Id,
                    StaffId = staff.Id
                });
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Inbox(int? id)
        {
            ViewBag.IsCompose = false;
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));

            CorrespondenceReadViewModel viewModel = new CorrespondenceReadViewModel
            {
                Correspondences = new List<Correspondence>()
            };
            List<Inbox> inboxes = await inboxManager.RetrieveAllWithStaffInfoAsync(staff.Id);

            foreach (Inbox inbox in inboxes)
            {
                correspondence = await correspondenceManager.RetrieveAsync(inbox.CorrespondenceId);
                correspondence.To = new List<Staff>
                {
                    staff
                };
                viewModel.Correspondences.Add(correspondence);
            }

            if (id.HasValue && viewModel.Correspondences.Any(c => c.Id == id))
            {
                viewModel.Correspondence = viewModel.Correspondences.First(c => c.Id == id.GetValueOrDefault());
            }

            return View(viewModel);
        }
        public async Task<IActionResult> Sent(int? id)
        {
            ViewBag.IsCompose = false;
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            CorrespondenceReadViewModel viewModel = new CorrespondenceReadViewModel
            {
                Correspondences = await correspondenceManager.RetrieveAllAsync(staff.Id)
            };
            List<Sent> sents = await sentManager.RetrieveAllAsync(staff.Id);

            foreach (Correspondence item in viewModel.Correspondences.ToList())
            {
                if (!sents.Any(i => i.CorrespondenceId == item.Id))
                    viewModel.Correspondences.Remove(item);
                else
                {
                    item.To = new List<Staff>();
                    foreach (To to in item.Tos)
                    {
                        staff = await staffManager.RetrieveAsync(to.StaffId.ToString());
                        item.To.Add(staff);
                    }
                }

            }

            if (id.HasValue && viewModel.Correspondences.Any(c => c.Id == id))
            {
                viewModel.Correspondence = viewModel.Correspondences.First(c => c.Id == id.GetValueOrDefault());
            }

            return View(viewModel);
        }
        public async Task<IActionResult> Draft()
        {
            ViewBag.IsCompose = false;
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            return View(await draftManager.RetrieveAllAsync(staff.Id));
        }
        
        [HttpPost]
        public async Task<JsonResult> Draft(string context, string title)
        {
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            
            try
            {
                await draftManager.InsertAsync(new Draft
                {
                    Title = title,
                    Context = context,
                    StaffId = staff.Id
                });
            }
            catch
            {
                return Json(false);
            }
            
            return Json(true);
        }
        public async Task<IActionResult> Trash(int? id)
        {
            ViewBag.IsCompose = false;
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            CorrespondenceReadViewModel viewModel = new CorrespondenceReadViewModel
            {
                Correspondences = new List<Correspondence>()
            };
            List<Trash> trashes = await trashManager.RetrieveAllAsync(staff.Id);

            foreach (Trash trash in trashes)
            {
                correspondence = await correspondenceManager.RetrieveAsync(trash.CorrespondenceId);
                correspondence.To = new List<Staff>();
                foreach (To to in correspondence.Tos)
                {
                    staff = await staffManager.RetrieveAsync(to.StaffId.ToString());
                    correspondence.To.Add(staff);
                }
                viewModel.Correspondences.Add(correspondence);
            }

            if (id.HasValue && viewModel.Correspondences.Any(c => c.Id == id))
            {
                viewModel.Correspondence = viewModel.Correspondences.First(c => c.Id == id.GetValueOrDefault());
            }

            return View(viewModel);
        }
        public IActionResult Email()
        {
            ViewBag.IsCompose = false;
            return View(new EmailViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Email([Bind("To, Subject, Textarea, Attachment")] EmailViewModel email)
        {
            ViewBag.IsCompose = false;
            if (ModelState.IsValid)
            {
                Institution ınstitution = await ınstitutionManager.RetrieveAsync(1);
                email.FilePaths = null;
                //email.From = HttpContext.Session.GetString("userNickname");
                //email.Password = HttpContext.Session.GetString("userPassword");
                email.From = ınstitution.Email;
                email.Password = ınstitution.Password;

                if (email.Attachment != null && email.Attachment.Count > 0)
                {
                    email.FilePaths = new List<string>();

                    foreach (IFormFile file in email.Attachment)
                    {
                        string fileName = await documentManager.SaveAsync(file, "email", "system");
                        string path = Path.Combine(hostEnvironment.WebRootPath, @"files\system\documents\email\" + fileName);

                        email.FilePaths.Add(path);
                    }
                }

                await messageService.Default(email);

                if (email.FilePaths != null && email.FilePaths.Count > 0)
                {
                    foreach (string path in email.FilePaths)
                    {
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    }
                }
                return View(new EmailViewModel());
            }
            return View(email);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCorrespondence(int id)
        {
            staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));

            if ((await trashManager.CheckAsync(id, staff.Id)))
            {
                await trashManager.DeleteAsync(await trashManager.RetrieveAsync(id, staff.Id));
                if (!(await trashManager.CheckAsync(id)))
                    await correspondenceManager.DeleteAsync(await correspondenceManager.RetrieveAsync(id));
            }
            else
            {
                await trashManager.InsertAsync(new Trash
                {
                    CorrespondenceId = id,
                    StaffId = staff.Id
                });
                
                if ((await inboxManager.CheckAsync(id, staff.Id)))
                    await inboxManager.DeleteAsync(await inboxManager.RetrieveAsync(id, staff.Id));
                
                if ((await sentManager.CheckAsync(id, staff.Id)))
                    await sentManager.DeleteAsync(await sentManager.RetrieveAsync(id, staff.Id));

            }
            return Json(null);
        }
        
        [HttpPost]
        public async Task<JsonResult> RemoveFromDraft(int id)
        {
            draft = await draftManager.RetrieveAsync(id);

            await draftManager.DeleteAsync(draft);
            return Json(null);
        }
    }
}