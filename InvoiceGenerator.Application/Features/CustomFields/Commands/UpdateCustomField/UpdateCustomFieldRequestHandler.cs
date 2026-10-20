using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.UpdateCustomField
{
    public class UpdateCustomFieldRequestHandler : IRequestHandler<UpdateCustomFieldRequest, CustomFieldDto?>
    {
        private readonly ICustomFieldRepository _customFieldRepository;

        public UpdateCustomFieldRequestHandler(ICustomFieldRepository customFieldRepository)
        {
            _customFieldRepository = customFieldRepository;
        }

        public async Task<CustomFieldDto?> Handle(UpdateCustomFieldRequest request, CancellationToken cancellationToken)
        {
            var customField = await _customFieldRepository.GetByIdAsync(request.Id, cancellationToken);
            if (customField is null)
            {
                return null;
            }

            customField.Name = request.Name;
            customField.Type = request.Type;
            customField.Enabled = request.Enabled;

            await _customFieldRepository.UpdateAsync(customField, cancellationToken);
            return CustomFieldDto.FromEntity(customField);
        }
    }
}
