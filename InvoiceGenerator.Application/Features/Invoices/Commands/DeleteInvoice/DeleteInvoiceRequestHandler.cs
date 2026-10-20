using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceRequestHandler : IRequestHandler<DeleteInvoiceRequest, bool>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public DeleteInvoiceRequestHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<bool> Handle(DeleteInvoiceRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (invoice is null)
            {
                return false;
            }

            // Invoice items and custom field values are removed by cascade delete
            await _invoiceRepository.DeleteAsync(invoice, cancellationToken);
            return true;
        }
    }
}
