using InvoiceGenerator.Application.Features.Invoices.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoicePdf
{
    // Returns null when no invoice with the given Id exists
    public record GetInvoicePdfRequest(int Id) : IRequest<InvoicePdfDto?>;
}
