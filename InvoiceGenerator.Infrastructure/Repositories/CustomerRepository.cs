using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(InvoiceGeneratorDbContext context)
            : base(context)
        {
        }

        public async Task<bool> HasInvoicesAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices.AnyAsync(i => i.CustomerId == customerId, cancellationToken);
        }
    }
}
