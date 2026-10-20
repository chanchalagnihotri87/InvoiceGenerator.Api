using InvoiceGenerator.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace InvoiceGenerator.Infrastructure.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public SmtpEmailSender(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            var mimeMessage = BuildMimeMessage(message);

            if (!string.IsNullOrWhiteSpace(_settings.PickupDirectory))
            {
                await WriteToPickupDirectoryAsync(mimeMessage, cancellationToken);
                return;
            }

            if (string.IsNullOrWhiteSpace(_settings.SmtpServer))
            {
                throw new InvalidOperationException("Email is not configured. Set EmailSettings:SmtpServer (or EmailSettings:PickupDirectory for local testing).");
            }

            using var client = new SmtpClient { CheckCertificateRevocation = _settings.CheckCertificateRevocation };
            // Auto picks implicit TLS on 465 and STARTTLS when the server offers it
            await client.ConnectAsync(_settings.SmtpServer, _settings.Port, SecureSocketOptions.Auto, cancellationToken);
            if (!string.IsNullOrEmpty(_settings.Username))
            {
                await client.AuthenticateAsync(_settings.Username, _settings.Password ?? string.Empty, cancellationToken);
            }
            await client.SendAsync(mimeMessage, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }

        private MimeMessage BuildMimeMessage(EmailMessage message)
        {
            if (string.IsNullOrWhiteSpace(_settings.SenderEmail))
            {
                throw new InvalidOperationException("Email is not configured. Set EmailSettings:SenderEmail.");
            }

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            mimeMessage.To.Add(new MailboxAddress(message.ToName ?? string.Empty, message.To));
            mimeMessage.Subject = message.Subject;

            var body = new BodyBuilder { TextBody = message.Body };
            foreach (var attachment in message.Attachments)
            {
                body.Attachments.Add(attachment.FileName, attachment.Content, ContentType.Parse(attachment.ContentType));
            }
            mimeMessage.Body = body.ToMessageBody();

            return mimeMessage;
        }

        private async Task WriteToPickupDirectoryAsync(MimeMessage mimeMessage, CancellationToken cancellationToken)
        {
            var directory = Path.GetFullPath(_settings.PickupDirectory!);
            Directory.CreateDirectory(directory);

            var path = Path.Combine(directory, $"{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid():N}.eml");
            await mimeMessage.WriteToAsync(path, cancellationToken);
        }
    }
}
