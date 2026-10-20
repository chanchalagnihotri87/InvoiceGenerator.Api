using InvoiceGenerator.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure.Services
{
    public class InvoiceNumberGenerator : IInvoiceNumberGenerator
    {
        public const string SequenceName = "InvoiceNumbers";

        private readonly InvoiceGeneratorDbContext _context;

        public InvoiceNumberGenerator(InvoiceGeneratorDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAsync(CancellationToken cancellationToken = default)
        {
            // A database sequence hands out each value once, even under concurrent requests.
            // ToListAsync (not SingleAsync) keeps EF from wrapping the SQL in a subquery, where NEXT VALUE FOR is not allowed.
            var values = await _context.Database
                .SqlQueryRaw<long>($"SELECT NEXT VALUE FOR [{SequenceName}] AS [Value]")
                .ToListAsync(cancellationToken);

            return $"INV-{values.Single():D6}";
        }
    }
}
