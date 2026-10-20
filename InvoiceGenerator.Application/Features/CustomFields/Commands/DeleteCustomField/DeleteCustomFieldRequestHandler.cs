using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.DeleteCustomField
{
    public class DeleteCustomFieldRequestHandler : IRequestHandler<DeleteCustomFieldRequest, bool>
    {
        private readonly ICustomFieldRepository _customFieldRepository;

        public DeleteCustomFieldRequestHandler(ICustomFieldRepository customFieldRepository)
        {
            _customFieldRepository = customFieldRepository;
        }

        public async Task<bool> Handle(DeleteCustomFieldRequest request, CancellationToken cancellationToken)
        {
            var customField = await _customFieldRepository.GetByIdAsync(request.Id, cancellationToken);
            if (customField is null)
            {
                return false;
            }

            await _customFieldRepository.DeleteAsync(customField, cancellationToken);
            return true;
        }
    }
}
