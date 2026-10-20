using InvoiceGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.EntityConfigurations
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(ii => ii.Id);

            builder.Property(ii => ii.UnitPrice)
                .HasPrecision(18, 2);

            // Computed in the domain, not persisted
            builder.Ignore(ii => ii.TotalPrice);

            builder.HasOne(ii => ii.Item)
                .WithMany()
                .HasForeignKey(ii => ii.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
