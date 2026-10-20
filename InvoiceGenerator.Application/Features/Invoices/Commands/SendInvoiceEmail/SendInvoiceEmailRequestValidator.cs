using FluentValidation;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.SendInvoiceEmail
{
    public class SendInvoiceEmailRequestValidator : AbstractValidator<SendInvoiceEmailRequest>
    {
        public SendInvoiceEmailRequestValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0);
            RuleFor(r => r.To).EmailAddress().MaximumLength(256).When(r => r.To is not null);
            RuleFor(r => r.Subject).NotEmpty().MaximumLength(200).When(r => r.Subject is not null);
            RuleFor(r => r.Message).NotEmpty().MaximumLength(5000).When(r => r.Message is not null);
        }
    }
}
