using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Features.Customers.Dtos
{
    public record CustomerDto(int Id, string Name, string Address, string Email, string? Phone)
    {
        public static CustomerDto FromEntity(Customer customer) =>
            new(customer.Id, customer.Name, customer.Address, customer.Email, customer.Phone);
    }
}
