using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Commands.CreateInvoice;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.SaveAndSendInvoice
{
    // Creates the invoice, then emails it. Email is optional; its fields override the defaults of SendInvoiceEmailRequest
    public record SaveAndSendInvoiceRequest(CreateInvoiceRequest Invoice, InvoiceEmailOptions? Email)
        : IRequest<Result<InvoiceSavedAndSentDto>>;

    public record InvoiceEmailOptions(string? To, string? Subject, string? Message);
}
