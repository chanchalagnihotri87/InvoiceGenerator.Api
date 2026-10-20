using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Queries.GetCustomFieldById
{
    // Returns null when no custom field with the given Id exists
    public record GetCustomFieldByIdRequest(int Id) : IRequest<CustomFieldDto?>;
}
