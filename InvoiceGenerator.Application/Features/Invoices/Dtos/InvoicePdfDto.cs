namespace InvoiceGenerator.Application.Features.Invoices.Dtos
{
    public record InvoicePdfDto(string FileName, byte[] Content);
}
