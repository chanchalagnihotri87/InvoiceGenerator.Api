using FluentValidation;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
    {
        public CreateInvoiceRequestValidator()
        {
            Include(new InvoiceInputValidator());
        }
    }
}
