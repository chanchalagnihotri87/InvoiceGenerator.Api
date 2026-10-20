using FluentValidation;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.CreateCustomField
{
    public class CreateCustomFieldRequestValidator : AbstractValidator<CreateCustomFieldRequest>
    {
        public CreateCustomFieldRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
            RuleFor(r => r.Type).IsInEnum();
        }
    }
}
