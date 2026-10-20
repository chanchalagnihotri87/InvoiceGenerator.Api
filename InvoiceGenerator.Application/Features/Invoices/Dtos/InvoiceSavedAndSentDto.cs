namespace InvoiceGenerator.Application.Features.Invoices.Dtos
{
    // The invoice is always saved; EmailSent tells whether the email also went out
    public record InvoiceSavedAndSentDto(InvoiceDto Invoice, bool EmailSent, string? SentTo, string? EmailError);
}
