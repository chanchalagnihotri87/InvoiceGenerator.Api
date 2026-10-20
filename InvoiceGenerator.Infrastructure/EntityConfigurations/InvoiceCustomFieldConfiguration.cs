using InvoiceGenerator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceGenerator.Infrastructure.EntityConfigurations
{
    public class InvoiceCustomFieldConfiguration : IEntityTypeConfiguration<InvoiceCustomField>
    {
        public void Configure(EntityTypeBuilder<InvoiceCustomField> builder)
        {
            builder.HasKey(icf => icf.Id);

            builder.Property(icf => icf.Value)
                .IsRequired()
                .HasMaxLength(500);

            // An invoice holds at most one value per custom field
            builder.HasIndex(icf => new { icf.InvoiceId, icf.CustomFieldId })
                .IsUnique();

            builder.HasOne(icf => icf.CustomField)
                .WithMany()
                .HasForeignKey(icf => icf.CustomFieldId)
                .OnDelete(DeleteBehavior.Restrict);

            // Match CustomField's soft-delete filter so values of deleted fields are hidden consistently
            builder.HasQueryFilter(icf => !icf.CustomField.IsDeleted);
        }
    }
}
