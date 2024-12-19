using CycleShare.Server.Dtos;
using CycleShare.Server.Helpers;
using CycleShare.Server.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace CycleShare.Server.Services
{
    public class MailService : IMailService
    {
        private readonly EmailSettings _mailSettings;
        private readonly IWebHostEnvironment _env;
        private readonly EmailEntitySettings _forgotPasswordSettings;

        public MailService(EmailSettings mailSettings,
                           IWebHostEnvironment env, 
                           EmailEntitySettings forgotPasswordSettings)
        {
            this._mailSettings = mailSettings;
            this._env = env;
            this._forgotPasswordSettings = forgotPasswordSettings;
        }

        public void SendForgotPasswordEmail(EmailDto email)
        {
            email.Endpoint = _forgotPasswordSettings.Endpoint;
            email.Subject = _forgotPasswordSettings.MailSubject;
            var template = GetTemplate("ForgotPasswordEmail.html");
            string body = string.Format(template, $"{email.FirstName} {email.LastName}", $"{email.Endpoint}?token={email.Token}");
            SendEmailAsync(email, body).Wait();
        }

        private string GetTemplate(string fileName)
        {
            string[] paths = { _env.WebRootPath, "Templates", "EmailTemplates", fileName };
            var templatePath = Path.Combine(paths);
            string template;
            using (StreamReader SourceReader = File.OpenText(templatePath))
            {
                template = SourceReader.ReadToEnd();
            }
            return template;
        }

        private async Task SendEmailAsync(EmailDto email, string body)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_mailSettings.FromEmail, _mailSettings.FromName)
                };
                mail.To.Add(new MailAddress(email.EmailAddress));

                mail.Subject = email.Subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_mailSettings.Domain, _mailSettings.Port))
                {
                    smtp.Credentials = new NetworkCredential(_mailSettings.UsernameEmail, _mailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sending an email", ex);
            }
        }
    }
}
