using InvoiceGenerator.Application.Features.Customers.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Queries.GetCustomers
{
    public record GetCustomersRequest : IRequest<IReadOnlyList<CustomerDto>>;
}
