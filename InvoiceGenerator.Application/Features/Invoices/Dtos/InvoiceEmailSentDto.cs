namespace InvoiceGenerator.Application.Features.Invoices.Dtos
{
    public record InvoiceEmailSentDto(int InvoiceId, string InvoiceNumber, string To);
}
