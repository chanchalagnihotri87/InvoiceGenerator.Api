using InvoiceGenerator.Application.Features.Customers.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Commands.CreateCustomer
{
    public record CreateCustomerRequest(string Name, string Address, string Email, string? Phone) : IRequest<CustomerDto>;
}
