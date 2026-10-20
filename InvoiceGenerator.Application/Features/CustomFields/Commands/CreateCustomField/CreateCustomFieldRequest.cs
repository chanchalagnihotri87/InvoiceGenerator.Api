using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using InvoiceGenerator.Common.Enums;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.CreateCustomField
{
    public record CreateCustomFieldRequest(string Name, CustomFieldType Type, bool Enabled = true) : IRequest<CustomFieldDto>;
}
