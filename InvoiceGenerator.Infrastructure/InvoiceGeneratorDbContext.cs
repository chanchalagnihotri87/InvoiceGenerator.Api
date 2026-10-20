using InvoiceGenerator.Domain.Entities;
using InvoiceGenerator.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Infrastructure
{
    public class InvoiceGeneratorDbContext : DbContext
    {
        public InvoiceGeneratorDbContext(DbContextOptions<InvoiceGeneratorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<CustomField> CustomFields => Set<CustomField>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceCustomField> InvoiceCustomFields => Set<InvoiceCustomField>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<Item> Items => Set<Item>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvoiceGeneratorDbContext).Assembly);

            modelBuilder.HasSequence<long>(InvoiceNumberGenerator.SequenceName)
                .StartsAt(1)
                .IncrementsBy(1);
        }
    }
}
