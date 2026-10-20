using InvoiceGenerator.Application.Features.Invoices.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoiceById
{
    // Returns null when no invoice with the given Id exists
    public record GetInvoiceByIdRequest(int Id) : IRequest<InvoiceDto?>;
}
