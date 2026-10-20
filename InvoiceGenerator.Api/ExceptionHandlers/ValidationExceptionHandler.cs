using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace InvoiceGenerator.Api.ExceptionHandlers
{
    // Turns FluentValidation failures from the MediatR pipeline into a 400 ValidationProblem response
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validationException)
            {
                return false;
            }

            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).Distinct().ToArray());

            await Results.ValidationProblem(errors).ExecuteAsync(httpContext);
            return true;
        }
    }
}
