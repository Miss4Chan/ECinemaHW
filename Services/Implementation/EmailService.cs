using ECinema.Domain;
using ECinema.Domain.Domain_Models;
using ECinema.Services.Interface;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        public EmailService(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }
        public async Task SendEmailAsync(List<EmailMessage> allMails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            foreach (var item in allMails)
            {
                var emailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(emailSettings.SendersName, emailSettings.SmtpUserName),
                    Subject = item.Subject
                };

                emailMessage.From.Add(new MailboxAddress(emailSettings.EmailDisplayName, emailSettings.SmtpUserName));

                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = item.Content };

                emailMessage.To.Add(new MailboxAddress(item.MailTo, item.MailTo));

                messages.Add(emailMessage);
            }

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOptions = emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await smtp.ConnectAsync(emailSettings.SmtpServer, emailSettings.SmtpServerPort, socketOptions);

                    if (!string.IsNullOrEmpty(emailSettings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(emailSettings.SmtpUserName, emailSettings.SmtpPassword);
                    }

                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }

                    await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
