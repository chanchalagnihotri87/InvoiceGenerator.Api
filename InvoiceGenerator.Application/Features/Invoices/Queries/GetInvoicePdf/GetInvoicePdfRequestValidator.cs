using FluentValidation;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoicePdf
{
    public class GetInvoicePdfRequestValidator : AbstractValidator<GetInvoicePdfRequest>
    {
        public GetInvoicePdfRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
