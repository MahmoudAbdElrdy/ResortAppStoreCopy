using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Sentry;

namespace CRM.Services.Helpers
{
    public static class EmailTemplates
    {
        static IWebHostEnvironment _hostingEnvironment;
        static string template;
        static string plainTextTestEmailTemplate;


        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public static string ConfirmEmail(string recepientName, string link, string userId)
        {
            if (template == null)
                template = ReadPhysicalFile("/wwwroot/EmailTemplates/ConfirmEmail.template");


            string emailMessage = template
                .Replace("{user}", recepientName)
                .Replace("{link}", link)
                .Replace("{userId}", userId);

            return emailMessage;
        }
        public static string VerifyCode(string recepientName, string link, string code)
        {
            if (template == null)
                template = ReadPhysicalFile("/wwwroot/EmailTemplates/VerifyCode.template");


            string emailMessage = template
                .Replace("{user}", recepientName)
                .Replace("{link}", link)
                .Replace("{code}", code);

            return emailMessage;
        }
        public static string AddPassword(string recepientName, string link)
        {
            if (template == null)
                template = ReadPhysicalFile("/wwwroot/EmailTemplates/AddPassword.template");


            string emailMessage = template
                .Replace("{user}", recepientName)
                .Replace("{link}", link)
               ;

            return emailMessage;
        }
        public static string ForgetPassword(string recepientName, string link, string userId)
        {
            if (template == null)
                template = ReadPhysicalFile("/Files/EmailTemplates/ForgetPassword.template");


            string emailMessage = template
                .Replace("{user}", recepientName)
                .Replace("{link}", link)
              //  .Replace("{userId}", userId)
                ;

            return emailMessage;
        }
        public static string GoToApplication(string recepientName, string link,string userName)
        {
            if (template == null)
                template = ReadPhysicalFile("/wwwroot/EmailTemplates/GoToApplication.template");


            string emailMessage = template
                .Replace("{user}", recepientName)
                .Replace("{link}", link)
                 .Replace("{userName}", userName);
           

            return emailMessage;
        }

        public static string SetPassword(string recepientName, string link, string userId)
        {
            if (template == null)
                template = ReadPhysicalFile("/wwwroot/EmailTemplates/SetPassword.template");


            string emailMessage = template
                .Replace("{user}", recepientName)
                .Replace("{link}", link)
                .Replace("{userId}", userId);

            return emailMessage;
        }

        private static string ReadPhysicalFile(string path)
        {
            if (_hostingEnvironment == null)
                throw new InvalidOperationException($"{nameof(EmailTemplates)} is not initialized");

            IFileInfo fileInfo = _hostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Template file located at \"{path}\" was not found");

            using (var fs = fileInfo.CreateReadStream())
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
