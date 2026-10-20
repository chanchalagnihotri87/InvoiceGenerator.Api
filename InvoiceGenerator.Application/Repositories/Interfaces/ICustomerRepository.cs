using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Repositories.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<bool> HasInvoicesAsync(int customerId, CancellationToken cancellationToken = default);
    }
}
