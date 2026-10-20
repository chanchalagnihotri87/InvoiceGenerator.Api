using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Commands.SendInvoiceEmail;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.SaveAndSendInvoice
{
    public class SaveAndSendInvoiceRequestHandler : IRequestHandler<SaveAndSendInvoiceRequest, Result<InvoiceSavedAndSentDto>>
    {
        private readonly ISender _sender;
        private readonly ILogger<SaveAndSendInvoiceRequestHandler> _logger;

        public SaveAndSendInvoiceRequestHandler(ISender sender, ILogger<SaveAndSendInvoiceRequestHandler> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task<Result<InvoiceSavedAndSentDto>> Handle(SaveAndSendInvoiceRequest request, CancellationToken cancellationToken)
        {
            var created = await _sender.Send(request.Invoice, cancellationToken);
            if (!created.IsSuccess)
            {
                return created.Error!;
            }

            var invoice = created.Value!;
            var email = request.Email;

            try
            {
                var sent = await _sender.Send(
                    new SendInvoiceEmailRequest(invoice.Id, email?.To, email?.Subject, email?.Message),
                    cancellationToken);

                return sent.IsSuccess
                    ? new InvoiceSavedAndSentDto(invoice, true, sent.Value!.To, null)
                    : new InvoiceSavedAndSentDto(invoice, false, null, sent.Error!.Message);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                // The invoice is already saved, so report the failure instead of returning 500 and inviting a duplicate retry
                _logger.LogError(ex, "Invoice {InvoiceNumber} was saved but the email could not be sent", invoice.InvoiceNumber);
                return new InvoiceSavedAndSentDto(invoice, false, null, "The invoice was saved but the email could not be sent.");
            }
        }
    }
}
