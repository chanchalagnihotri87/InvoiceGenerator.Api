using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.UpdateInvoice
{
    // Items and CustomFields replace the invoice's existing lines entirely; the generated InvoiceNumber cannot be changed
    public record UpdateInvoiceRequest(
        int Id,
        DateTime InvoiceDate,
        DateTime DueDate,
        int CustomerId,
        decimal DiscountAmount,
        decimal TaxPercentage,
        IReadOnlyList<InvoiceItemInput> Items,
        IReadOnlyList<InvoiceCustomFieldInput>? CustomFields) : IRequest<Result<InvoiceDto>>, IInvoiceInput;
}
