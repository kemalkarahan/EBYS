using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Adapter.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EBYS.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IFileCompressionService fileCompression;
        private readonly IDocumentManager documentManager;
        private readonly IDocumentAttachmentManager documentAttachmentManager;
        private readonly IDocumentRelatedManager documentRelatedManager;
        private readonly IStaffManager staffManager;
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public DocumentController(IFileCompressionService fileCompression,
                                  IDocumentManager documentManager,
                                  IDocumentAttachmentManager documentAttachmentManager,
                                  IDocumentRelatedManager documentRelatedManager,
                                  IStaffManager staffManager,
                                  IHostingEnvironment hostingEnvironment)
        {
            this.fileCompression = fileCompression;
            this.documentManager = documentManager;
            this.documentAttachmentManager = documentAttachmentManager;
            this.documentRelatedManager = documentRelatedManager;
            this.staffManager = staffManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            List<Document> documents = await documentManager.RetrieveAllAsync(staff.Id);
            return View(documents);
        }
        public IActionResult AddDocument()
        {
            AddNewDocumentViewModel documentViewModel = new AddNewDocumentViewModel();
            return View(documentViewModel);
        }

        [HttpPost]
        [RequestSizeLimit(1073741824)]
        public async Task<IActionResult> AddDocument([Bind("Document, AttachedExistedDocuments, RelatedExistedDocument, AttachedNewDocuments, RelatedNewDocuments")] AddNewDocumentViewModel documentViewModel)
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            Document newDocument = new Document();

            if (ModelState.IsValid)
            {
                newDocument.OwnerId = staff.Id;
                newDocument.CreatedAt = DateTime.Now;
                newDocument.Title = documentViewModel.Document.FileName;
                newDocument.FileName = await documentManager.SaveAsync(documentViewModel.Document, staff.Nickname);
                newDocument.IsAddition = false;
                newDocument.HasAdditions = false;
                if (documentViewModel.AttachedExistedDocuments != null || documentViewModel.RelatedExistedDocument != null || documentViewModel.AttachedNewDocuments != null || documentViewModel.RelatedNewDocuments != null)
                    newDocument.HasAdditions = true;
                await documentManager.InsertAsync(newDocument);
                Document document = await documentManager.RetrieveLastAsync();

                if (documentViewModel.AttachedExistedDocuments != null)
                {
                    foreach (string attachmentId in documentViewModel.AttachedExistedDocuments)
                    {
                        await documentAttachmentManager.InsertAsync(new DocumentAttachment
                        {
                            DocumentAttachmentId = int.Parse(attachmentId),
                            DocumentId = document.Id
                        });
                    }
                }
                if (documentViewModel.RelatedExistedDocument != null)
                {
                    foreach (string relatedId in documentViewModel.AttachedExistedDocuments)
                    {
                        await documentRelatedManager.InsertAsync(new DocumentRelated
                        {
                            DocumentRelatedId = int.Parse(relatedId),
                            DocumentId = document.Id
                        });
                    }
                }
                if (documentViewModel.AttachedNewDocuments != null)
                {
                    List<Document> documents = await documentManager.PrepareDocumentsAsync(documentViewModel.AttachedNewDocuments, staff);

                    foreach (Document newAddedDocument in documents)
                    {
                        await documentAttachmentManager.InsertAsync(new DocumentAttachment
                        {
                            DocumentAttachmentId = newAddedDocument.Id,
                            DocumentId = document.Id
                        });
                    }
                }
                if (documentViewModel.RelatedNewDocuments != null)
                {
                    List<Document> documents = await documentManager.PrepareDocumentsAsync(documentViewModel.RelatedNewDocuments, staff);
                    foreach (Document newAddedDocument in documents)
                    {
                        await documentRelatedManager.InsertAsync(new DocumentRelated
                        {
                            DocumentRelatedId = newAddedDocument.Id,
                            DocumentId = document.Id
                        });
                    }
                }
                return RedirectToAction("Index");
            }
            return View(documentViewModel);
        }

        [Obsolete]
        public async Task<FileResult> Download(int id)
        {
            string fileName = "Evraklar.zip";
            
            byte[] finalResult = await fileCompression.Compression(id, fileName, HttpContext.Session.GetString("userNickname"));

            if (System.IO.File.Exists(Path.Combine(hostingEnvironment.WebRootPath + @"\files\system\documents\download", fileName)))
            {
                System.IO.File.Delete(Path.Combine(hostingEnvironment.WebRootPath + @"\files\system\documents\download", fileName));
            }

            if (finalResult == null || !finalResult.Any())
            {
                throw new Exception(String.Format("Dosya bulunamadı."));
            }

            return File(finalResult, "application/zip", fileName);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            string nickname = HttpContext.Session.GetString("userNickname");
            Document document = await documentManager.RetrieveWithAdditionsAsync(id);
            var isDeleted = true;
            try
            {
                if (document.DocumentAttachments != null)
                {
                    foreach (DocumentAttachment attachment in document.DocumentAttachments.ToList())
                    {
                        if ((await documentAttachmentManager.CheckAsync(attachment.DocumentAttachmentId)))
                        {
                            Document tempDocument = await documentManager.RetrieveAsync(attachment.DocumentAttachmentId);
                            await documentAttachmentManager.DeleteAsync(attachment);
                            await documentManager.DeleteAsync(tempDocument);
                            documentManager.DeleteDocument(tempDocument, "additions");
                        }
                        else
                        {
                            break;
                        }

                    }
                }
                if (document.DocumentRelateds != null)
                {
                    foreach (DocumentRelated related in document.DocumentRelateds.ToList())
                    {
                        if ((await documentRelatedManager.CheckAsync(related.DocumentRelatedId)))
                        {
                            Document tempDocument = await documentManager.RetrieveAsync(related.DocumentRelatedId);
                            await documentRelatedManager.DeleteAsync(related);
                            await documentManager.DeleteAsync(tempDocument);
                            documentManager.DeleteDocument(tempDocument, "additions");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                await documentManager.DeleteAsync(document);
                documentManager.DeleteDocument(document, nickname);
            }
            catch (Exception)
            {
                isDeleted = false;
                return Json(isDeleted);
            }
            return Json(isDeleted);
        }

        public async Task<IActionResult> ShareDocument(int id)
        {
            Staff staff = await staffManager.RetrieveAsync(HttpContext.Session.GetString("userNickname"));
            Document document = await documentManager.RetrieveAsync(id);
            ShareDocumentViewModel shareDocument = new ShareDocumentViewModel
            {
                DocumentName = document.FileName,
                DocumentId = id,
                Staffs = await staffManager.RetrieveAllAsync()
            };
            shareDocument.Staffs.Remove(staff);
            return View(shareDocument);
        }

        [HttpPost]
        public async Task<IActionResult> ShareDocument([Bind("DocumentId, DocumentName, SendingList")] ShareDocumentViewModel shareDocument)
        {
            if (ModelState.IsValid)
            {
                await documentManager.ShareDocumentAsync(shareDocument.SendingList, shareDocument.DocumentId, HttpContext.Session.GetString("userNickname"));
                return RedirectToAction("Index");
            }
            return View(shareDocument);
        }
    }
}