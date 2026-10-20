using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using InvoiceGenerator.Common.Enums;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.UpdateCustomField
{
    // Returns null when no custom field with the given Id exists
    public record UpdateCustomFieldRequest(int Id, string Name, CustomFieldType Type, bool Enabled) : IRequest<CustomFieldDto?>;
}
