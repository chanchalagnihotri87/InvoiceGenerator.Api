using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceRequestHandler : IRequestHandler<UpdateInvoiceRequest, Result<InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ICustomFieldRepository _customFieldRepository;

        public UpdateInvoiceRequestHandler(
            IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository,
            IItemRepository itemRepository,
            ICustomFieldRepository customFieldRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _itemRepository = itemRepository;
            _customFieldRepository = customFieldRepository;
        }

        public async Task<Result<InvoiceDto>> Handle(UpdateInvoiceRequest request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetWithDetailsByIdAsync(request.Id, cancellationToken);
            if (invoice is null)
            {
                return Error.NotFound($"Invoice {request.Id} does not exist.");
            }

            var lines = await InvoiceLinesResolver.ResolveAsync(
                request.CustomerId,
                request.Items, request.CustomFields,
                _customerRepository, _itemRepository, _customFieldRepository, cancellationToken);
            if (!lines.IsSuccess)
            {
                return lines.Error!;
            }

            invoice.InvoiceDate = request.InvoiceDate;
            invoice.DueDate = request.DueDate;
            invoice.CustomerId = request.CustomerId;
            invoice.DiscountAmount = request.DiscountAmount;
            invoice.TaxPercentage = request.TaxPercentage;

            // Removed lines become orphans of a required relationship, so EF deletes them on save
            invoice.InvoiceItems.Clear();
            lines.Value!.Items.ForEach(invoice.InvoiceItems.Add);
            invoice.InvoiceCustomFields.Clear();
            lines.Value.CustomFields.ForEach(invoice.InvoiceCustomFields.Add);

            await _invoiceRepository.UpdateAsync(invoice, cancellationToken);

            var updated = await _invoiceRepository.GetWithDetailsByIdAsync(invoice.Id, cancellationToken);
            return InvoiceDto.FromEntity(updated!);
        }
    }
}
