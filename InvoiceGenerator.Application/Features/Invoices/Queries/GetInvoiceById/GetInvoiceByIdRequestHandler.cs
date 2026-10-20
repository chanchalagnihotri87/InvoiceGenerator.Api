using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdRequestHandler : IRequestHandler<GetInvoiceByIdRequest, InvoiceDto?>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceByIdRequestHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDto?> Handle(GetInvoiceByIdRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetWithDetailsByIdAsync(request.Id, cancellationToken);
            return invoice is null ? null : InvoiceDto.FromEntity(invoice);
        }
    }
}
