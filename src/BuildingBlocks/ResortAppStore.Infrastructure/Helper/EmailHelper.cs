using MailKit.Security;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public interface IEmailHelper
    {
        void SendEmailAsync(string to, string body, string subject);
    }
    public  class EmailHelper: IEmailHelper
    {
        private readonly IConfiguration _configuration;
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmailAsync(string to, string body, string subject)
        {
            try
            {
                var EmailOwner = _configuration.GetValue<string>("EmailConfiguration:emailOwner");
                var PasswordOwner = _configuration.GetValue<string>("EmailConfiguration:PasswordOwner");
               // var senderEmail = _configuration.GetValue<string>("CompanyMailConfigurations:AuthAccounts:SenderEmail");
                System.Net.Mail.MailMessage MailMessageObj = new System.Net.Mail.MailMessage();
                
                MailMessageObj.To.Add(to);
                MailMessageObj.Subject = subject;
                MailMessageObj.IsBodyHtml = true;
                MailMessageObj.Body = body;
                MailMessageObj.BodyEncoding = System.Text.Encoding.UTF8;
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");
                SmtpServer.Port = 587;//587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(EmailOwner, PasswordOwner);
                MailMessageObj.From = new System.Net.Mail.MailAddress(EmailOwner);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(MailMessageObj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -" + ex);
            }

        }
    }
}
