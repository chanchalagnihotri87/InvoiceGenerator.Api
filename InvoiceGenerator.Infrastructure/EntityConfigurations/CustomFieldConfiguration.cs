using InvoiceGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.EntityConfigurations
{
    public class CustomFieldConfiguration : IEntityTypeConfiguration<CustomField>
    {
        public void Configure(EntityTypeBuilder<CustomField> builder)
        {
            builder.HasKey(cf => cf.Id);

            builder.Property(cf => cf.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Store the enum as text so the column stays readable and survives enum reordering
            builder.Property(cf => cf.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            // Soft delete: exclude deleted fields from all queries by default
            builder.HasQueryFilter(cf => !cf.IsDeleted);
        }
    }
}
