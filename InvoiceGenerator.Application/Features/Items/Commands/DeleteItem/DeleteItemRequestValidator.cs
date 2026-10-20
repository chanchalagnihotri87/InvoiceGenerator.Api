using FluentValidation;

namespace InvoiceGenerator.Application.Features.Items.Commands.DeleteItem
{
    public class DeleteItemRequestValidator : AbstractValidator<DeleteItemRequest>
    {
        public DeleteItemRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
