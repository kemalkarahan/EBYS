using Business.Abstract;
using Entities.Concrete;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using Services.Adapter.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.Adapter.Concrete
{
    public class CSharpCodeAdapter : IFileCompressionService
    {
        private readonly IDocumentManager documentManager;
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public CSharpCodeAdapter(IDocumentManager documentManager, IHostingEnvironment hostingEnvironment)
        {
            this.documentManager = documentManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Obsolete]
        public async Task<byte[]> Compression(int fileId, string zipFileName, string fileDirectoryName)
        {
            string directory = Path.Combine(hostingEnvironment.WebRootPath + @"\files\system\documents\download");
            string tempOutput = Path.Combine(directory, zipFileName);

            using (ZipOutputStream zipOutputStream = new ZipOutputStream(File.Create(tempOutput)))
            {
                zipOutputStream.SetLevel(9);

                byte[] buffer = new byte[4096];

                Document document = await documentManager.RetrieveWithAdditionsAsync(fileId);

                string tempDirectory = Path.Combine(hostingEnvironment.WebRootPath + @"\files\user\documents\" + fileDirectoryName + @"\" + document.FileName);
                ZipEntry entry = new ZipEntry(Path.GetFileName(tempDirectory).Substring(9))
                {
                    DateTime = DateTime.Now,
                    IsUnicodeText = true
                };

                zipOutputStream.PutNextEntry(entry);

                using (FileStream fileStream = File.OpenRead(tempDirectory))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fileStream.Read(buffer, 0, buffer.Length);
                        zipOutputStream.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }

                if (document.DocumentAttachments != null)
                {
                    foreach (var attachment in document.DocumentAttachments)
                    {
                        Document tempDocument = await documentManager.RetrieveAsync(attachment.DocumentAttachmentId);
                        tempDirectory = Path.Combine(hostingEnvironment.WebRootPath + @"\files\user\documents\additions\" + tempDocument.FileName);
                        entry = new ZipEntry(Path.GetFileName(tempDirectory).Substring(9))
                        {
                            DateTime = DateTime.Now,
                            IsUnicodeText = true
                        };

                        zipOutputStream.PutNextEntry(entry);

                        using FileStream fileStream = File.OpenRead(tempDirectory);
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fileStream.Read(buffer, 0, buffer.Length);
                            zipOutputStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }

                if (document.DocumentRelateds != null)
                {
                    foreach (var related in document.DocumentRelateds)
                    {
                        Document tempDocument = await documentManager.RetrieveAsync(related.DocumentRelatedId);
                        tempDirectory = Path.Combine(hostingEnvironment.WebRootPath + @"\files\user\documents\additions\" + tempDocument.FileName);
                        entry = new ZipEntry(Path.GetFileName(tempDirectory).Substring(9))
                        {
                            DateTime = DateTime.Now,
                            IsUnicodeText = true
                        };

                        zipOutputStream.PutNextEntry(entry);

                        using FileStream fileStream = File.OpenRead(tempDirectory);
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fileStream.Read(buffer, 0, buffer.Length);
                            zipOutputStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }

                    zipOutputStream.Finish();
                    zipOutputStream.Flush();
                    zipOutputStream.Close();
                }
            }

            return File.ReadAllBytes(tempOutput);
        }
    }
}
