using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.DeleteCustomField
{
    // Returns false when no custom field with the given Id exists
    public record DeleteCustomFieldRequest(int Id) : IRequest<bool>;
}
