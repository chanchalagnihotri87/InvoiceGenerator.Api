using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Items.Commands.DeleteItem
{
    public class DeleteItemRequestHandler : IRequestHandler<DeleteItemRequest, bool>
    {
        private readonly IItemRepository _itemRepository;

        public DeleteItemRequestHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<bool> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
            if (item is null)
            {
                return false;
            }

            await _itemRepository.DeleteAsync(item, cancellationToken);
            return true;
        }
    }
}
