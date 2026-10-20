using InvoiceGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.EntityConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(i => i.InvoiceNumber)
                .IsUnique();

            builder.Property(i => i.DiscountAmount)
                .HasPrecision(18, 2);

            builder.Property(i => i.TaxPercentage)
                .HasPrecision(5, 2);

            // Computed in the domain, not persisted
            builder.Ignore(i => i.SubTotal);
            builder.Ignore(i => i.SubTotalAfterDiscount);
            builder.Ignore(i => i.TaxAmount);
            builder.Ignore(i => i.Total);

            builder.HasOne(i => i.Customer)
                .WithMany()
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.InvoiceItems)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.InvoiceCustomFields)
                .WithOne(icf => icf.Invoice)
                .HasForeignKey(icf => icf.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
