using FluentValidation;
using InvoiceGenerator.Application.Features.Invoices.Commands.CreateInvoice;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.SaveAndSendInvoice
{
    public class SaveAndSendInvoiceRequestValidator : AbstractValidator<SaveAndSendInvoiceRequest>
    {
        public SaveAndSendInvoiceRequestValidator()
        {
            RuleFor(r => r.Invoice).NotNull().SetValidator(new CreateInvoiceRequestValidator());

            // Same limits as SendInvoiceEmailRequestValidator
            When(r => r.Email is not null, () =>
            {
                RuleFor(r => r.Email!.To).EmailAddress().MaximumLength(256).When(r => r.Email!.To is not null);
                RuleFor(r => r.Email!.Subject).NotEmpty().MaximumLength(200).When(r => r.Email!.Subject is not null);
                RuleFor(r => r.Email!.Message).NotEmpty().MaximumLength(5000).When(r => r.Email!.Message is not null);
            });
        }
    }
}
