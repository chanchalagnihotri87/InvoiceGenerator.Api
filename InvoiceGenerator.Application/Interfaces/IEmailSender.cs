namespace InvoiceGenerator.Application.Interfaces
{
    public record EmailAttachment(string FileName, string ContentType, byte[] Content);

    public record EmailMessage(
        string To,
        string? ToName,
        string Subject,
        string Body,
        IReadOnlyList<EmailAttachment> Attachments);

    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
    }
}
