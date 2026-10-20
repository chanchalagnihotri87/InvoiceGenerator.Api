using FluentValidation;

namespace InvoiceGenerator.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerRequestValidator : AbstractValidator<DeleteCustomerRequest>
    {
        public DeleteCustomerRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
