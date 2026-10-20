using InvoiceGenerator.Application.Features.Customers.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, CustomerDto?>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerRequestHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto?> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (customer is null)
            {
                return null;
            }

            customer.Name = request.Name;
            customer.Address = request.Address;
            customer.Email = request.Email;
            customer.Phone = request.Phone;

            await _customerRepository.UpdateAsync(customer, cancellationToken);
            return CustomerDto.FromEntity(customer);
        }
    }
}
