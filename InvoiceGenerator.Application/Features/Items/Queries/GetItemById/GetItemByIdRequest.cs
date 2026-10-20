using InvoiceGenerator.Application.Features.Items.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Queries.GetItemById
{
    // Returns null when no item with the given Id exists
    public record GetItemByIdRequest(int Id) : IRequest<ItemDto?>;
}
