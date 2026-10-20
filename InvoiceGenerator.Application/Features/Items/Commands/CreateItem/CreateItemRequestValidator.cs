using FluentValidation;

namespace InvoiceGenerator.Application.Features.Items.Commands.CreateItem
{
    public class CreateItemRequestValidator : AbstractValidator<CreateItemRequest>
    {
        public CreateItemRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(200);
            RuleFor(r => r.Description).MaximumLength(1000);
            RuleFor(r => r.Price).GreaterThanOrEqualTo(0);
        }
    }
}
