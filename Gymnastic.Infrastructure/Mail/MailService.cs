using Gymnastic.Infrastructure.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;


namespace Gymnastic.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile>? attachments)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.SenderEmail),
                Subject = subject,
            };

            mail.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();

            if (attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = body;
            mail.Body = builder.ToMessageBody();
            mail.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.SenderEmail));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.SmtpServer, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.SenderEmail, _mailSettings.SenderPassword);
            await smtp.SendAsync(mail);

            smtp.Disconnect(true);
        }
    }
}
