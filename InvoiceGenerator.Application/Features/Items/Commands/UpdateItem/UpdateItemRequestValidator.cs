using FluentValidation;

namespace InvoiceGenerator.Application.Features.Items.Commands.UpdateItem
{
    public class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
    {
        public UpdateItemRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty().MaximumLength(200);
            RuleFor(r => r.Description).MaximumLength(1000);
            RuleFor(r => r.Price).GreaterThanOrEqualTo(0);
        }
    }
}
