using Microsoft.Extensions.Configuration;
using SaudiEinvoiceService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.Services
{
    public class EMailService
    {
        private readonly IConfiguration _configuration;
        public EMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string toAddress, string invoiceDriveUrl, Setting setting, Guid billGuid)
        {
            try
            {
                //------------------------------------------------------------

                string smtpServer = _configuration.GetSection("smtpServer").Value ?? "";
                int smtpPort = Convert.ToInt32(_configuration.GetSection("smtpPort").Value);
                string smtpUsername = _configuration.GetSection("senderEmail").Value ?? "";
                string smtpPassword = _configuration.GetSection("password").Value ?? "";

                // Create the attachment
                //List<Attachment> attachmentList = new List<Attachment>();

                bool enableGoogleDrive = Convert.ToBoolean(_configuration.GetSection("enable").Value);
                string body = "";

                if (enableGoogleDrive)
                {
                    body = "عزيزي العميل , "+"<br>"+ "تم اصدار فاتورة الكترونية يمكن الاطلاع عليها من الرابط التالي  "+"<br>"+invoiceDriveUrl +"<br>"+"مع أطيب تمنياتنا";
                }
                else
                {
                    body = "عزيزي العميل , " + "<br>" + "تم اصدار فاتورة الكترونية يمكن الاطلاع عليها في المرفق  " +  "<br>" + "مع أطيب تمنياتنا";
                }

                // Create the email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    Subject = "فاتورة الكترونية",
                    Body = body,
                    IsBodyHtml = true
                };


                mailMessage.To.Add(toAddress);

                // Attach the file
                //mailMessage.Attachments.Add(attachment);
                
                Attachment? attachment = null;
                if (!enableGoogleDrive) 
                {
                    string filePath =setting.PdfDirectory+ @"\"+billGuid.ToString()+".pdf";
                    attachment = new Attachment(filePath, MediaTypeNames.Application.Octet);

                    mailMessage.Attachments.Add(attachment);
                }
                

                // Set up the SMTP client
                using (SmtpClient smtpClient = new SmtpClient(smtpServer))
                {
                    smtpClient.Port = smtpPort;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Timeout = 120000;

                    // Send the email
                    smtpClient.Send(mailMessage);
                }

                // Dispose of the attachment after sending the email
                if (attachment != null) 
                {
                    attachment.Dispose();
                }
                return true;
                
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
            
            
    }
}
