using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Interfaces;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoicePdf
{
    public class GetInvoicePdfRequestHandler : IRequestHandler<GetInvoicePdfRequest, InvoicePdfDto?>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoicePdfGenerator _invoicePdfGenerator;

        public GetInvoicePdfRequestHandler(IInvoiceRepository invoiceRepository, IInvoicePdfGenerator invoicePdfGenerator)
        {
            _invoiceRepository = invoiceRepository;
            _invoicePdfGenerator = invoicePdfGenerator;
        }

        public async Task<InvoicePdfDto?> Handle(GetInvoicePdfRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetWithDetailsByIdAsync(request.Id, cancellationToken);
            if (invoice is null)
            {
                return null;
            }

            var content = _invoicePdfGenerator.Generate(invoice);
            return new InvoicePdfDto($"{invoice.InvoiceNumber}.pdf", content);
        }
    }
}
