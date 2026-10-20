using FluentValidation;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceRequestValidator : AbstractValidator<UpdateInvoiceRequest>
    {
        public UpdateInvoiceRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
            Include(new InvoiceInputValidator());
        }
    }
}
