using InvoiceGenerator.Application.Features.Customers.Dtos;
using InvoiceGenerator.Application.Repositories.Interfaces;
using MediatR;

namespace InvoiceGenerator.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersRequestHandler : IRequestHandler<GetCustomersRequest, IReadOnlyList<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersRequestHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IReadOnlyList<CustomerDto>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);
            return customers.Select(CustomerDto.FromEntity).ToList();
        }
    }
}
