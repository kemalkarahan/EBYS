using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Services.NativeServices.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Utilities.Abstract;

namespace Services.NativeServices.Concrete
{
    public class MessageService : IMessageService
    {
        private Institution institution;
        private readonly IUtility utilities;
        private readonly IInstitutionManager institutionManager;
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public MessageService(IUtility utilities,
                              IInstitutionManager institutionManager,
                              IHostingEnvironment hostingEnvironment)
        {
            this.utilities = utilities;
            this.institutionManager = institutionManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        //MailMessage.Attachments Property https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.mailmessage.attachments?view=netcore-3.1
        //Attachment Class https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.attachment?view=netcore-3.1
        [Obsolete]
        void BuildMail(string subjectText, string bodyText, string from, string to, string password, List<string> filePaths = null)
        {
            string bcc, cc, subject, body;
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(bodyText);
            body = stringBuilder.ToString();
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(from)
            };
            mailMessage.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mailMessage.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mailMessage.Bcc.Add(new MailAddress(cc));
            }
            if (filePaths != null)
            {
                foreach (string file in filePaths)
                {
                    mailMessage.Attachments.Add(new Attachment(file));
                }
            }
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SendMail(mailMessage, from, password);
        }

        [Obsolete]
        void SendMail(MailMessage mailMessage, string userName, string password)
        {
            SmtpClient smtpClient = new SmtpClient
            {
                Host = institution.SmtpHost,
                Port = institution.SmtpPort,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(userName, password)
            };

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Obsolete]
        public async Task Default(EmailViewModel email)
        {
            institution = await institutionManager.RetrieveAsync(1);

            string from, to;
            from = (email.From + institution.Domain).Trim();
            to = (email.To + institution.Domain).Trim();

            string body = File.ReadAllText(hostingEnvironment.ContentRootPath + @"\MessageTemplates\"
                   + "Default"
                   + ".cshtml");

            body = body.Replace("@ViewBag.Content", email.Textarea);
            body = body.ToString();
            BuildMail(email.Subject, body, from, to, email.Password, email.FilePaths);
        }

        [Obsolete]
        public async Task<string> NewStaffAdded(string nickname, string name, string lastName)
        {
            institution = await institutionManager.RetrieveAsync(1);
            string from, to, password = Guid.NewGuid().ToString();
            
            password = password.Substring(0, 8);
            
            from = (institution.Email + institution.Domain).Trim();
            to = (nickname + institution.Domain).Trim();
            string body = File.ReadAllText(hostingEnvironment.ContentRootPath + @"\MessageTemplates\"
                   + "NewStaffAdded"
                   + ".cshtml");
            body = body.Replace("@ViewBag.Nickname", nickname);
            body = body.Replace("@ViewBag.Password", password);
            body = body.Replace("@ViewBag.UserFullName", name + " " + lastName);
            body = body.ToString();
            BuildMail("Yeni Hesap Oluşturuldu", body, from, to, institution.Password);

            return utilities.ConvertToHashCode(password);
        }

        [Obsolete]
        public async Task PasswordReset(Staff staff)
        {
            institution = await institutionManager.RetrieveAsync(1);
            string from, to;
            from = (institution.Email + institution.Domain).Trim();
            to = (staff.Nickname + institution.Domain).Trim();
            string body = File.ReadAllText(hostingEnvironment.ContentRootPath + @"\MessageTemplates\"
                   + "PasswordReset"
                   + ".cshtml");
            string id = utilities.ConvertToHashCode(staff.Nickname);
            string url = "http://localhost:50118" + "/Account/PasswordReset?value=" + id;
            body = body.Replace("@ViewBag.Link", url);
            body = body.Replace("@ViewBag.UserFullName", staff.Name + " " + staff.LastName);
            body = body.ToString();
            BuildMail("Şifre Resetleme Talebi", body, from, to, institution.Password);
        }
    }
}
