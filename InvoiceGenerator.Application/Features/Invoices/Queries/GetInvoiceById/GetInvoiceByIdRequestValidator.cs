using FluentValidation;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdRequestValidator : AbstractValidator<GetInvoiceByIdRequest>
    {
        public GetInvoiceByIdRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
