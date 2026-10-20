using InvoiceGenerator.Api.Endpoints;
using InvoiceGenerator.Api.ExceptionHandlers;

namespace InvoiceGenerator.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddProblemDetails();
            services.AddExceptionHandler<ValidationExceptionHandler>();

            return services;
        }

        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapItemEndpoints();
            app.MapCustomFieldEndpoints();
            app.MapCustomerEndpoints();
            app.MapInvoiceEndpoints();

            return app;
        }
    }
}
