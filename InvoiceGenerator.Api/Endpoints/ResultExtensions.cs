using InvoiceGenerator.Application.Common;

namespace InvoiceGenerator.Api.Endpoints
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResult(this Error error) => error.Type switch
        {
            ErrorType.NotFound => Results.NotFound(new { message = error.Message }),
            ErrorType.Conflict => Results.Conflict(new { message = error.Message }),
            _ => Results.BadRequest(new { message = error.Message })
        };
    }
}
