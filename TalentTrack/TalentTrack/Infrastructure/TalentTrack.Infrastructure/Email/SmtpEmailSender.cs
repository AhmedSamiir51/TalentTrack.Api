using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TalentTrack.Core.Interfaces;
using TMG.SharedKernel.Models;

namespace TalentTrack.Infrastructure.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailConfiguration _mailserverConfiguration;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(ILogger<SmtpEmailSender> logger, IOptions<EmailConfiguration> mailserverOptions)
        {
            _mailserverConfiguration = mailserverOptions.Value!;
            _logger = logger;
        }

        public async Task SendEmailAsync(string EmailAddress, string subject, string Message)
        {
            try
            {
                var fromMail = new MailAddress(_mailserverConfiguration.FromAddress, _mailserverConfiguration.FromName);
                var toMail = new MailAddress(EmailAddress);
                var smtp = new SmtpClient
                {
                    Host = _mailserverConfiguration.SmtpServer,
                    Port = _mailserverConfiguration.SmtpPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromMail.Address, _mailserverConfiguration.SmtpPassword)
                };

                using (var message = new MailMessage(fromMail, toMail)
                {
                    Subject = subject,
                    Body = Message,
                    IsBodyHtml = true
                })
                {
                    _logger.LogInformation("Sending email to {to} from {from} with subject {subject} using {type}.", toMail, fromMail, subject, this.ToString());

                    await smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message + " " + ex.InnerException?.Message);
            }
        }

    }
}
