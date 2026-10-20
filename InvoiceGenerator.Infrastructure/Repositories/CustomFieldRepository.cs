using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class CustomFieldRepository : BaseRepository<CustomField>, ICustomFieldRepository
    {
        public CustomFieldRepository(InvoiceGeneratorDbContext context)
            : base(context)
        {
        }

        // Soft delete: keep the row and hide it via the IsDeleted query filter
        public override async Task DeleteAsync(CustomField entity, CancellationToken cancellationToken = default)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity, cancellationToken);
        }
    }
}
