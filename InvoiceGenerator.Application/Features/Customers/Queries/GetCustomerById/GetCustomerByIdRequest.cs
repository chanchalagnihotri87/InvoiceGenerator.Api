using InvoiceGenerator.Application.Features.Customers.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Queries.GetCustomerById
{
    // Returns null when no customer with the given Id exists
    public record GetCustomerByIdRequest(int Id) : IRequest<CustomerDto?>;
}
