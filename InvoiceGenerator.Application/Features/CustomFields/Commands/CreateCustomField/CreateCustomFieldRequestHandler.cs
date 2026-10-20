using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.CreateCustomField
{
    public class CreateCustomFieldRequestHandler : IRequestHandler<CreateCustomFieldRequest, CustomFieldDto>
    {
        private readonly ICustomFieldRepository _customFieldRepository;

        public CreateCustomFieldRequestHandler(ICustomFieldRepository customFieldRepository)
        {
            _customFieldRepository = customFieldRepository;
        }

        public async Task<CustomFieldDto> Handle(CreateCustomFieldRequest request, CancellationToken cancellationToken)
        {
            var customField = new CustomField
            {
                Name = request.Name,
                Type = request.Type,
                Enabled = request.Enabled
            };

            await _customFieldRepository.CreateAsync(customField, cancellationToken);
            return CustomFieldDto.FromEntity(customField);
        }
    }
}
