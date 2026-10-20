using InvoiceGenerator.Application.Features.Customers.Commands.CreateCustomer;
using InvoiceGenerator.Application.Features.Customers.Commands.DeleteCustomer;
using InvoiceGenerator.Application.Features.Customers.Commands.UpdateCustomer;
using InvoiceGenerator.Application.Features.Customers.Dtos;
using InvoiceGenerator.Application.Features.Customers.Queries.GetCustomerById;
using InvoiceGenerator.Application.Features.Customers.Queries.GetCustomers;
using MediatR;

namespace InvoiceGenerator.Api.Endpoints
{
    public static class CustomerEndpoints
    {
        public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/customers").WithTags("Customers");

            group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
                Results.Ok(await sender.Send(new GetCustomersRequest(), cancellationToken)))
                .WithName("GetCustomers")
                .Produces<IReadOnlyList<CustomerDto>>();

            group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var customer = await sender.Send(new GetCustomerByIdRequest(id), cancellationToken);
                return customer is null ? Results.NotFound() : Results.Ok(customer);
            })
                .WithName("GetCustomerById")
                .Produces<CustomerDto>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (CreateCustomerRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var customer = await sender.Send(request, cancellationToken);
                return Results.CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
            })
                .WithName("CreateCustomer")
                .Produces<CustomerDto>(StatusCodes.Status201Created);

            group.MapPut("/{id:int}", async (int id, UpdateCustomerRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                // The route id wins over any Id sent in the body
                var customer = await sender.Send(request with { Id = id }, cancellationToken);
                return customer is null ? Results.NotFound() : Results.Ok(customer);
            })
                .WithName("UpdateCustomer")
                .Produces<CustomerDto>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapDelete("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new DeleteCustomerRequest(id), cancellationToken);
                return result switch
                {
                    DeleteCustomerResult.Deleted => Results.NoContent(),
                    DeleteCustomerResult.HasInvoices => Results.Conflict(new { message = "Customer has invoices and cannot be deleted." }),
                    _ => Results.NotFound()
                };
            })
                .WithName("DeleteCustomer")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status409Conflict);

            return app;
        }
    }
}
