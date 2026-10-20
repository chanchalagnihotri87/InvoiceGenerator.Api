using InvoiceGenerator.Common.Enums;
using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Features.CustomFields.Dtos
{
    public record CustomFieldDto(int Id, string Name, CustomFieldType Type, bool Enabled)
    {
        public static CustomFieldDto FromEntity(CustomField customField) =>
            new(customField.Id, customField.Name, customField.Type, customField.Enabled);
    }
}
