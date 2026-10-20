using FluentValidation;

namespace InvoiceGenerator.Application.Features.CustomFields.Commands.DeleteCustomField
{
    public class DeleteCustomFieldRequestValidator : AbstractValidator<DeleteCustomFieldRequest>
    {
        public DeleteCustomFieldRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
