using InvoiceGenerator.Application.Common;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Interfaces;
using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;
using MediatR;

namespace InvoiceGenerator.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceRequestHandler : IRequestHandler<CreateInvoiceRequest, Result<InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ICustomFieldRepository _customFieldRepository;
        private readonly IInvoiceNumberGenerator _invoiceNumberGenerator;

        public CreateInvoiceRequestHandler(
            IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository,
            IItemRepository itemRepository,
            ICustomFieldRepository customFieldRepository,
            IInvoiceNumberGenerator invoiceNumberGenerator)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _itemRepository = itemRepository;
            _customFieldRepository = customFieldRepository;
            _invoiceNumberGenerator = invoiceNumberGenerator;
        }

        public async Task<Result<InvoiceDto>> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
        {
            var lines = await InvoiceLinesResolver.ResolveAsync(
                request.CustomerId,
                request.Items, request.CustomFields,
                _customerRepository, _itemRepository, _customFieldRepository, cancellationToken);
            if (!lines.IsSuccess)
            {
                return lines.Error!;
            }

            // Generated only after validation passes, so rejected requests don't consume numbers
            var invoice = new Invoice
            {
                InvoiceNumber = await _invoiceNumberGenerator.GenerateAsync(cancellationToken),
                InvoiceDate = request.InvoiceDate,
                DueDate = request.DueDate,
                CustomerId = request.CustomerId,
                DiscountAmount = request.DiscountAmount,
                TaxPercentage = request.TaxPercentage,
                InvoiceItems = lines.Value!.Items,
                InvoiceCustomFields = lines.Value.CustomFields
            };

            await _invoiceRepository.CreateAsync(invoice, cancellationToken);

            var created = await _invoiceRepository.GetWithDetailsByIdAsync(invoice.Id, cancellationToken);
            return InvoiceDto.FromEntity(created!);
        }
    }
}
