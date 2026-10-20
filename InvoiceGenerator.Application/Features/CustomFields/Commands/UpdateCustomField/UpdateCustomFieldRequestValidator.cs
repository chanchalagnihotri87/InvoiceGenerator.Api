using FluentValidation;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.UpdateCustomField
{
    public class UpdateCustomFieldRequestValidator : AbstractValidator<UpdateCustomFieldRequest>
    {
        public UpdateCustomFieldRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
            RuleFor(r => r.Type).IsInEnum();
        }
    }
}
