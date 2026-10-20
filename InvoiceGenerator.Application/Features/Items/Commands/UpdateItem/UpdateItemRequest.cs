using InvoiceGenerator.Application.Features.Items.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Commands.UpdateItem
{
    // Returns null when no item with the given Id exists
    public record UpdateItemRequest(int Id, string Name, string? Description, decimal Price) : IRequest<ItemDto?>;
}
