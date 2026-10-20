using FluentValidation;

namespace InvoiceGenerator.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(r => r.Name).NotEmpty().MaximumLength(200);
            RuleFor(r => r.Address).NotEmpty().MaximumLength(500);
            RuleFor(r => r.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(r => r.Phone).MaximumLength(20);
        }
    }
}
