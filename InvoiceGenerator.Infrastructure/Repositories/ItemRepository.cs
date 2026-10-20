using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Infrastructure.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(InvoiceGeneratorDbContext context)
            : base(context)
        {
        }
    }
}
