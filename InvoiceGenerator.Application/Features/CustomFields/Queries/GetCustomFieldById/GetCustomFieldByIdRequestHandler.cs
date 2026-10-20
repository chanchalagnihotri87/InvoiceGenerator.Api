using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Queries.GetCustomFieldById
{
    public class GetCustomFieldByIdRequestHandler : IRequestHandler<GetCustomFieldByIdRequest, CustomFieldDto?>
    {
        private readonly ICustomFieldRepository _customFieldRepository;

        public GetCustomFieldByIdRequestHandler(ICustomFieldRepository customFieldRepository)
        {
            _customFieldRepository = customFieldRepository;
        }

        public async Task<CustomFieldDto?> Handle(GetCustomFieldByIdRequest request, CancellationToken cancellationToken)
        {
            var customField = await _customFieldRepository.GetByIdAsync(request.Id, cancellationToken);
            return customField is null ? null : CustomFieldDto.FromEntity(customField);
        }
    }
}
