using InvoiceGenerator.Application.Features.Items.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemRequestHandler : IRequestHandler<CreateItemRequest, ItemDto>
    {
        private readonly IItemRepository _itemRepository;

        public CreateItemRequestHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDto> Handle(CreateItemRequest request, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            await _itemRepository.CreateAsync(item, cancellationToken);
            return ItemDto.FromEntity(item);
        }
    }
}
