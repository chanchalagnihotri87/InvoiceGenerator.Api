using InvoiceGenerator.Application.Features.Items.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Queries.GetItems
{
    public class GetItemsRequestHandler : IRequestHandler<GetItemsRequest, IReadOnlyList<ItemDto>>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemsRequestHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<IReadOnlyList<ItemDto>> Handle(GetItemsRequest request, CancellationToken cancellationToken)
        {
            var items = await _itemRepository.GetAllAsync(cancellationToken);
            return items.Select(ItemDto.FromEntity).ToList();
        }
    }
}
