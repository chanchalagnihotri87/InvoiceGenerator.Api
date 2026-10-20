using FluentValidation;

namespace InvoiceGenerator.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty().MaximumLength(200);
            RuleFor(r => r.Address).NotEmpty().MaximumLength(500);
            RuleFor(r => r.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(r => r.Phone).MaximumLength(20);
        }
    }
}
