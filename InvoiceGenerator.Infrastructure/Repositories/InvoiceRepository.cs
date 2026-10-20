using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(InvoiceGeneratorDbContext context)
            : base(context)
        {
        }

        public async Task<Invoice?> GetWithDetailsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await WithDetails().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Invoice>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await WithDetails()
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync(cancellationToken);
        }

        // Split query avoids a cartesian explosion from including two collections
        private IQueryable<Invoice> WithDetails() =>
            _dbSet
                .Include(i => i.Customer)
                .Include(i => i.InvoiceItems).ThenInclude(ii => ii.Item)
                .Include(i => i.InvoiceCustomFields).ThenInclude(icf => icf.CustomField)
                .AsSplitQuery();
    }
}
