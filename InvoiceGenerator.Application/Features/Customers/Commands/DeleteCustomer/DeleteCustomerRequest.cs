using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Commands.DeleteCustomer
{
    public record DeleteCustomerRequest(int Id) : IRequest<DeleteCustomerResult>;

    public enum DeleteCustomerResult
    {
        Deleted,
        NotFound,
        HasInvoices
    }
}
