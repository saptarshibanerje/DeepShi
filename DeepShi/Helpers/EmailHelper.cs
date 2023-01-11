using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DeepShiShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DeepShiApp.Helpers
{
    public class EmailHelper
    {
        private string _SmtpClient;
        private int _SmtpClientPort;
        private readonly int SmtpPort;
        private readonly string DisplayName;
        private readonly string SenderEmail;
        private readonly string SenderPassword;
        private readonly ILogger<EmailHelper> _logger;
        public EmailHelper(IConfiguration configuration, ILogger<EmailHelper> logger)
        {
            DisplayName = configuration.GetSection("AppConfig")["SmtpDisplayName"];
            _SmtpClient = configuration.GetSection("AppConfig")["SmtpClient"];
            _SmtpClientPort = Convert.ToInt32(configuration.GetSection("AppConfig")["SmtpClientPort"]);
            SmtpPort = Convert.ToInt32(configuration.GetSection("AppConfig")["SmtpPort"] ?? "0");
            SenderEmail = configuration.GetSection("AppConfig")["SenderEmail"];
            SenderPassword = configuration.GetSection("AppConfig")["SenderPassword"];
            _logger = logger;
        }
        public async Task<bool> SendEmail(EmailModel objEmailModel)
        {
            try
            {
                if (objEmailModel != null)
                {
                    MailMessage mm = new MailMessage();
                    mm.From = new MailAddress(SenderEmail, DisplayName);
                    mm.IsBodyHtml = objEmailModel.IsEmailBodyHtml;
                    mm.Body = objEmailModel.EmailBody;
                    mm.Subject = objEmailModel.EmailSubject;
                    mm.Priority = objEmailModel.EmailPriority;

                    foreach (var address in objEmailModel?.EmailTo?.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mm.To.Add(address);
                    }
                    foreach (var address in objEmailModel?.EmailCc?.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mm.CC.Add(address);
                    }
                    foreach (var address in objEmailModel?.EmailBcc?.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        mm.Bcc.Add(address);
                    }
                    SmtpClient smtp = new SmtpClient(_SmtpClient, _SmtpClientPort)
                    {
                        Port = SmtpPort,
                        UseDefaultCredentials = false,
                        //EnableSsl = true,
                        Credentials = new NetworkCredential(SenderEmail, SenderPassword),
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                    };
                    await smtp.SendMailAsync(mm);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendEmail");
                return false;
            }

        }
    }
}
