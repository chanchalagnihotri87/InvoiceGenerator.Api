using InvoiceGenerator.Application.Features.Customers.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Commands.UpdateCustomer
{
    // Returns null when no customer with the given Id exists
    public record UpdateCustomerRequest(int Id, string Name, string Address, string Email, string? Phone) : IRequest<CustomerDto?>;
}
