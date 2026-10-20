using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.SendInvoiceEmail
{
    // To defaults to the customer's email; Subject and Message replace the default text when set
    public record SendInvoiceEmailRequest(int Id, string? To, string? Subject, string? Message)
        : IRequest<Result<InvoiceEmailSentDto>>;
}
