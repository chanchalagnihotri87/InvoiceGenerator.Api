using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Commands.DeleteItem
{
    // Returns false when no item with the given Id exists
    public record DeleteItemRequest(int Id) : IRequest<bool>;
}
