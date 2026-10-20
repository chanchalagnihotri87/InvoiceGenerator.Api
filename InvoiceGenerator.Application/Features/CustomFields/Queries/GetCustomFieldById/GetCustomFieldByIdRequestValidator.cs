using FluentValidation;

namespace InvoiceGenerator.Application.Features.CustomFields.Queries.GetCustomFieldById
{
    public class GetCustomFieldByIdRequestValidator : AbstractValidator<GetCustomFieldByIdRequest>
    {
        public GetCustomFieldByIdRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
