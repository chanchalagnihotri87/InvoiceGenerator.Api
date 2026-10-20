using InvoiceGenerator.Application.Features.Invoices.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoices
{
    public record GetInvoicesRequest : IRequest<IReadOnlyList<InvoiceDto>>;
}
