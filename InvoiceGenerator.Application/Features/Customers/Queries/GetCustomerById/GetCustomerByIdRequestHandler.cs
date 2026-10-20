using InvoiceGenerator.Application.Features.Customers.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdRequest, CustomerDto?>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdRequestHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
            return customer is null ? null : CustomerDto.FromEntity(customer);
        }
    }
}
