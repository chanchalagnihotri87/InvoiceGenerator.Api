using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.DeleteInvoice
{
    // Returns false when no invoice with the given Id exists
    public record DeleteInvoiceRequest(int Id) : IRequest<bool>;
}
