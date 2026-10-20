using InvoiceGenerator.Application.Features.Items.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Commands.UpdateItem
{
    public class UpdateItemRequestHandler : IRequestHandler<UpdateItemRequest, ItemDto?>
    {
        private readonly IItemRepository _itemRepository;

        public UpdateItemRequestHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDto?> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
            if (item is null)
            {
                return null;
            }

            item.Name = request.Name;
            item.Description = request.Description;
            item.Price = request.Price;

            await _itemRepository.UpdateAsync(item, cancellationToken);
            return ItemDto.FromEntity(item);
        }
    }
}
