using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Repositories.Interfaces
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        // Loads Customer, InvoiceItems (with Item) and InvoiceCustomFields (with CustomField) so totals can be computed
        Task<Invoice?> GetWithDetailsByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Invoice>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    }
}
