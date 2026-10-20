using InvoiceGenerator.Application.Features.Items.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Queries.GetItemById
{
    public class GetItemByIdRequestHandler : IRequestHandler<GetItemByIdRequest, ItemDto?>
    {
        private readonly IItemRepository _itemRepository;

        public GetItemByIdRequestHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDto?> Handle(GetItemByIdRequest request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
            return item is null ? null : ItemDto.FromEntity(item);
        }
    }
}
