using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Business.Concrete.EntityFramework
{
    public class DocumentManager : IDocumentManager
    {
        private readonly IDocumentDal documentDal;
        private readonly IDocumentRelatedManager documentRelatedManager;
        private readonly IDocumentAttachmentManager documentAttachmentManager;
        private readonly IStaffManager staffManager;
        private readonly IHostingEnvironment hostEnvironment;

        public DocumentManager(IDocumentDal documentDal,
                               IDocumentRelatedManager documentRelatedManager,
                               IDocumentAttachmentManager documentAttachmentManager,
                               IStaffManager staffManager,
                               IHostingEnvironment hostEnvironment
                               )
        {
            this.documentDal = documentDal;
            this.documentRelatedManager = documentRelatedManager;
            this.documentAttachmentManager = documentAttachmentManager;
            this.staffManager = staffManager;
            this.hostEnvironment = hostEnvironment;
        }
        public async Task<bool> CheckAsync(int id)
        {
            return await documentDal.Check(d => d.Id == id);
        }

        public async Task DeleteAsync(Document entity)
        {
            await documentDal.Delete(entity);
        }

        public async Task InsertAsync(Document entity)
        {
            await documentDal.Insert(entity);
        }

        public async Task<Document> RetrieveLastAsync()
        {
           return await documentDal.RetrieveLast();
        }

        public async Task<Document> RetrieveAsync(int id)
        {
            return await documentDal.Retrieve(d => d.Id == id);
        }

        public async Task<Document> RetrieveWithAdditionsAsync(int id)
        {
            return await documentDal.RetrieveWithAdditions(d => d.Id == id);
        }

        public async Task<List<Document>> RetrieveAllAsync(int id)
        {
            return await documentDal.RetrieveAll(d => d.OwnerId == id && d.IsAddition == false);
        }

        public async Task UpdateAsync(Document entity)
        {
            await documentDal.Update(entity);
        }

        public async Task<List<Document>> PrepareDocumentsAsync(List<IFormFile> files, Staff staff)
        {
            List<Document> documents = new List<Document>();

            foreach (var file in files)
            {
                Document document = new Document
                {
                    CreatedAt = DateTime.Now,
                    Title = file.FileName,
                    OwnerId = staff.Id,
                    HasAdditions = false,
                    IsAddition = true,
                    FileName = await SaveAsync(file, "additions")
                };
                await documentDal.Insert(document);
                document = await documentDal.RetrieveLast();
                documents.Add(document);
            }

            return documents;
        }

        public async Task<string> SaveAsync(IFormFile file, string folder, string directoryFolder = "user")
        {
            string directory = Path.Combine(hostEnvironment.WebRootPath, @"files\" + directoryFolder + @"\documents\" + folder + @"\");
            string fileName = Guid.NewGuid().ToString().Substring(0, 8) + "_" + file.FileName;

            string fullPath = Path.Combine(directory, fileName);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public void DeleteDocument(Document document, string folder, string directoryFolder = "user")
        {
            string directory = Path.Combine(hostEnvironment.WebRootPath, @"files\" + directoryFolder + @"\documents\" + folder + @"\");
            string fileName = document.FileName;

            string fullPath = Path.Combine(directory, fileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public async Task ShareDocumentAsync(List<string> nicknameLİst, int documentId, string staffNickname)
        {
            Document document = await RetrieveWithAdditionsAsync(documentId);
            string sourceFile = Path.Combine(hostEnvironment.WebRootPath, @"files\user\documents\" + staffNickname + @"\" + document.FileName);
            foreach (var folderName in nicknameLİst)
            {
                string destFile = Path.Combine(hostEnvironment.WebRootPath, @"files\user\documents\" + folderName + @"\" + document.FileName);

                File.Copy(sourceFile, destFile, true);
            }

            foreach (var owner in nicknameLİst)
            {
                Staff staff = await staffManager.RetrieveAsync(owner);
                Document sharedDocument = new Document
                {
                    CreatedAt = DateTime.Now,
                    FileName = document.FileName,
                    HasAdditions = document.HasAdditions,
                    IsAddition = document.IsAddition,
                    Title = document.Title,
                    OwnerId = staff.Id
                };

                await documentDal.Insert(sharedDocument);

                if (sharedDocument.HasAdditions)
                {
                    sharedDocument = await documentDal.RetrieveLast();

                    foreach (DocumentAttachment attachment in document.DocumentAttachments)
                    {
                        await documentAttachmentManager.InsertAsync(new DocumentAttachment
                        {
                            DocumentAttachmentId = attachment.DocumentAttachmentId,
                            DocumentId = sharedDocument.Id
                        });
                    }

                    foreach (DocumentRelated related in document.DocumentRelateds)
                    {
                        await documentRelatedManager.InsertAsync(new DocumentRelated
                        {
                            DocumentRelatedId = related.DocumentRelatedId,
                            DocumentId = sharedDocument.Id
                        });
                    }
                }
            }
        }
    }
}
