using FluentValidation;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceRequestValidator : AbstractValidator<DeleteInvoiceRequest>
    {
        public DeleteInvoiceRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
        }
    }
}
