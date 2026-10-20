using FluentValidation;

namespace InvoiceGenerator.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdRequestValidator : AbstractValidator<GetCustomerByIdRequest>
    {
        public GetCustomerByIdRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
