using FluentValidation;

namespace InvoiceGenerator.Application.Features.Items.Queries.GetItemById
{
    public class GetItemByIdRequestValidator : AbstractValidator<GetItemByIdRequest>
    {
        public GetItemByIdRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
