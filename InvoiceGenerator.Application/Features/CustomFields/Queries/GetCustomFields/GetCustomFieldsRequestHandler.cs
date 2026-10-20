using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.CustomFields.Queries.GetCustomFields
{
    public class GetCustomFieldsRequestHandler : IRequestHandler<GetCustomFieldsRequest, IReadOnlyList<CustomFieldDto>>
    {
        private readonly ICustomFieldRepository _customFieldRepository;

        public GetCustomFieldsRequestHandler(ICustomFieldRepository customFieldRepository)
        {
            _customFieldRepository = customFieldRepository;
        }

        public async Task<IReadOnlyList<CustomFieldDto>> Handle(GetCustomFieldsRequest request, CancellationToken cancellationToken)
        {
            var customFields = await _customFieldRepository.GetAllAsync(cancellationToken);
            return customFields.Select(CustomFieldDto.FromEntity).ToList();
        }
    }
}
