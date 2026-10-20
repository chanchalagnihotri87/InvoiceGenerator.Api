using InvoiceGenerator.Application.Features.Invoices.Commands.CreateInvoice;
using InvoiceGenerator.Application.Features.Invoices.Commands.DeleteInvoice;
using InvoiceGenerator.Application.Features.Invoices.Commands.SaveAndSendInvoice;
using InvoiceGenerator.Application.Features.Invoices.Commands.SendInvoiceEmail;
using InvoiceGenerator.Application.Features.Invoices.Commands.UpdateInvoice;
using InvoiceGenerator.Application.Features.Invoices.Dtos;
using InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoiceById;
using InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoicePdf;
using InvoiceGenerator.Application.Features.Invoices.Queries.GetInvoices;
using MediatR;

namespace InvoiceGenerator.Api.Endpoints
{
    public static class InvoiceEndpoints
    {
        public static IEndpointRouteBuilder MapInvoiceEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/invoices").WithTags("Invoices");

            group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
                Results.Ok(await sender.Send(new GetInvoicesRequest(), cancellationToken)))
                .WithName("GetInvoices")
                .Produces<IReadOnlyList<InvoiceDto>>();

            group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var invoice = await sender.Send(new GetInvoiceByIdRequest(id), cancellationToken);
                return invoice is null ? Results.NotFound() : Results.Ok(invoice);
            })
                .WithName("GetInvoiceById")
                .Produces<InvoiceDto>()
                .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/{id:int}/pdf", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var pdf = await sender.Send(new GetInvoicePdfRequest(id), cancellationToken);
                return pdf is null ? Results.NotFound() : Results.File(pdf.Content, "application/pdf", pdf.FileName);
            })
                .WithName("GetInvoicePdf")
                .Produces(StatusCodes.Status200OK, contentType: "application/pdf")
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/{id:int}/send", async (int id, SendInvoiceEmailRequest? request, ISender sender, CancellationToken cancellationToken) =>
            {
                // The body is optional; the route id wins over any Id sent in it
                request = request is null ? new SendInvoiceEmailRequest(id, null, null, null) : request with { Id = id };
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccess ? Results.Ok(result.Value) : result.Error!.ToHttpResult();
            })
                .WithName("SendInvoiceEmail")
                .Produces<InvoiceEmailSentDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (CreateInvoiceRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccess
                    ? Results.CreatedAtRoute("GetInvoiceById", new { id = result.Value!.Id }, result.Value)
                    : result.Error!.ToHttpResult();
            })
                .WithName("CreateInvoice")
                .Produces<InvoiceDto>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPost("/save-and-send", async (SaveAndSendInvoiceRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccess
                    ? Results.CreatedAtRoute("GetInvoiceById", new { id = result.Value!.Invoice.Id }, result.Value)
                    : result.Error!.ToHttpResult();
            })
                .WithName("SaveAndSendInvoice")
                .Produces<InvoiceSavedAndSentDto>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            group.MapPut("/{id:int}", async (int id, UpdateInvoiceRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                // The route id wins over any Id sent in the body
                var result = await sender.Send(request with { Id = id }, cancellationToken);
                return result.IsSuccess ? Results.Ok(result.Value) : result.Error!.ToHttpResult();
            })
                .WithName("UpdateInvoice")
                .Produces<InvoiceDto>()
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound);

            group.MapDelete("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var deleted = await sender.Send(new DeleteInvoiceRequest(id), cancellationToken);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
                .WithName("DeleteInvoice")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);

            return app;
        }
    }
}
