using InvoiceGenerator.Application.Features.Items.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Queries.GetItems
{
    public record GetItemsRequest : IRequest<IReadOnlyList<ItemDto>>;
}
