using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoices
{
    public class GetInvoicesRequestHandler : IRequestHandler<GetInvoicesRequest, IReadOnlyList<InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoicesRequestHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IReadOnlyList<InvoiceDto>> Handle(GetInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetAllWithDetailsAsync(cancellationToken);
            return invoices.Select(InvoiceDto.FromEntity).ToList();
        }
    }
}
