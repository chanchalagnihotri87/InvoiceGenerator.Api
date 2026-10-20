using System.Globalization;
using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Interfaces;
using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.SendInvoiceEmail
{
    public class SendInvoiceEmailRequestHandler : IRequestHandler<SendInvoiceEmailRequest, Result<InvoiceEmailSentDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoicePdfGenerator _invoicePdfGenerator;
        private readonly IEmailSender _emailSender;

        public SendInvoiceEmailRequestHandler(
            IInvoiceRepository invoiceRepository,
            IInvoicePdfGenerator invoicePdfGenerator,
            IEmailSender emailSender)
        {
            _invoiceRepository = invoiceRepository;
            _invoicePdfGenerator = invoicePdfGenerator;
            _emailSender = emailSender;
        }

        public async Task<Result<InvoiceEmailSentDto>> Handle(SendInvoiceEmailRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetWithDetailsByIdAsync(request.Id, cancellationToken);
            if (invoice is null)
            {
                return Error.NotFound($"Invoice {request.Id} was not found.");
            }

            var to = request.To ?? invoice.Customer.Email;
            var pdf = _invoicePdfGenerator.Generate(invoice);

            var message = new EmailMessage(
                to,
                request.To is null ? invoice.Customer.Name : null,
                request.Subject ?? $"Invoice {invoice.InvoiceNumber}",
                request.Message ?? DefaultBody(invoice),
                [new EmailAttachment($"{invoice.InvoiceNumber}.pdf", "application/pdf", pdf)]);

            await _emailSender.SendAsync(message, cancellationToken);

            return new InvoiceEmailSentDto(invoice.Id, invoice.InvoiceNumber, to);
        }

        private static string DefaultBody(Invoice invoice) =>
            $"""
            Dear {invoice.Customer.Name},

            Please find attached invoice {invoice.InvoiceNumber} dated {FormatDate(invoice.InvoiceDate)} for {invoice.Total.ToString("N2", CultureInfo.InvariantCulture)}, due on {FormatDate(invoice.DueDate)}.

            Thank you for your business.
            """;

        private static string FormatDate(DateTime date) => date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
    }
}
