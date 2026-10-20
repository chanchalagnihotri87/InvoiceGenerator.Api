using InvoiceGenerator.Application.Features.CustomFields.Commands.CreateCustomField;
using InvoiceGenerator.Application.Features.CustomFields.Commands.DeleteCustomField;
using InvoiceGenerator.Application.Features.CustomFields.Commands.UpdateCustomField;
using InvoiceGenerator.Application.Features.CustomFields.Dtos;
using InvoiceGenerator.Application.Features.CustomFields.Queries.GetCustomFieldById;
using InvoiceGenerator.Application.Features.CustomFields.Queries.GetCustomFields;
using MediatR;

namespace InvoiceGenerator.Api.Endpoints
{
    public static class CustomFieldEndpoints
    {
        public static IEndpointRouteBuilder MapCustomFieldEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/customfields").WithTags("CustomFields");

            group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
                Results.Ok(await sender.Send(new GetCustomFieldsRequest(), cancellationToken)))
                .WithName("GetCustomFields")
                .Produces<IReadOnlyList<CustomFieldDto>>();

            group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var customField = await sender.Send(new GetCustomFieldByIdRequest(id), cancellationToken);
                return customField is null ? Results.NotFound() : Results.Ok(customField);
            })
                .WithName("GetCustomFieldById")
                .Produces<CustomFieldDto>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (CreateCustomFieldRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var customField = await sender.Send(request, cancellationToken);
                return Results.CreatedAtRoute("GetCustomFieldById", new { id = customField.Id }, customField);
            })
                .WithName("CreateCustomField")
                .Produces<CustomFieldDto>(StatusCodes.Status201Created);

            group.MapPut("/{id:int}", async (int id, UpdateCustomFieldRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                // The route id wins over any Id sent in the body
                var customField = await sender.Send(request with { Id = id }, cancellationToken);
                return customField is null ? Results.NotFound() : Results.Ok(customField);
            })
                .WithName("UpdateCustomField")
                .Produces<CustomFieldDto>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapDelete("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var deleted = await sender.Send(new DeleteCustomFieldRequest(id), cancellationToken);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
                .WithName("DeleteCustomField")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
