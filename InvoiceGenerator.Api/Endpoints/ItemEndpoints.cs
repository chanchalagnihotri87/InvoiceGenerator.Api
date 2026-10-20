using InvoiceGenerator.Application.Features.Items.Commands.CreateItem;
using InvoiceGenerator.Application.Features.Items.Commands.DeleteItem;
using InvoiceGenerator.Application.Features.Items.Commands.UpdateItem;
using InvoiceGenerator.Application.Features.Items.Dtos;
using InvoiceGenerator.Application.Features.Items.Queries.GetItemById;
using InvoiceGenerator.Application.Features.Items.Queries.GetItems;
using MediatR;

namespace InvoiceGenerator.Api.Endpoints
{
    public static class ItemEndpoints
    {
        public static IEndpointRouteBuilder MapItemEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/items").WithTags("Items");

            group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
                Results.Ok(await sender.Send(new GetItemsRequest(), cancellationToken)))
                .WithName("GetItems")
                .Produces<IReadOnlyList<ItemDto>>();

            group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var item = await sender.Send(new GetItemByIdRequest(id), cancellationToken);
                return item is null ? Results.NotFound() : Results.Ok(item);
            })
                .WithName("GetItemById")
                .Produces<ItemDto>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (CreateItemRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var item = await sender.Send(request, cancellationToken);
                return Results.CreatedAtRoute("GetItemById", new { id = item.Id }, item);
            })
                .WithName("CreateItem")
                .Produces<ItemDto>(StatusCodes.Status201Created);

            group.MapPut("/{id:int}", async (int id, UpdateItemRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                // The route id wins over any Id sent in the body
                var item = await sender.Send(request with { Id = id }, cancellationToken);
                return item is null ? Results.NotFound() : Results.Ok(item);
            })
                .WithName("UpdateItem")
                .Produces<ItemDto>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapDelete("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var deleted = await sender.Send(new DeleteItemRequest(id), cancellationToken);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
                .WithName("DeleteItem")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
