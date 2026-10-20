using InvoiceGenerator.Application.Features.Items.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Commands.CreateItem
{
    public record CreateItemRequest(string Name, string? Description, decimal Price) : IRequest<ItemDto>;
}
