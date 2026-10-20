using InvoiceGenerator.Application.Features.Customers.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Domain.Entities;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerRequestHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                Phone = request.Phone
            };

            await _customerRepository.CreateAsync(customer, cancellationToken);
            return CustomerDto.FromEntity(customer);
        }
    }
}
