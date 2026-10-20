using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, DeleteCustomerResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerRequestHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<DeleteCustomerResult> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null)
            {
                return DeleteCustomerResult.NotFound;
            }

            // Invoices reference customers with a restrict FK, so deleting would fail in the database
            if (await _customerRepository.HasInvoicesAsync(request.Id, cancellationToken))
            {
                return DeleteCustomerResult.HasInvoices;
            }

            await _customerRepository.DeleteAsync(customer, cancellationToken);
            return DeleteCustomerResult.Deleted;
        }
    }
}
