using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Queries.GetCustomFields
{
    public record GetCustomFieldsRequest : IRequest<IReadOnlyList<CustomFieldDto>>;
}
