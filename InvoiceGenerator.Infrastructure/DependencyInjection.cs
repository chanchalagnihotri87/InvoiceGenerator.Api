using InvoiceGenerator.Application.Interfaces;
using InvoiceGenerator.Application.Repositories.Interfaces;
using InvoiceGenerator.Infrastructure.Repositories;
using InvoiceGenerator.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceGenerator.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("InvoiceGeneratorDb")
                ?? throw new InvalidOperationException("Connection string 'InvoiceGeneratorDb' is not configured.");

            services.AddDbContext<InvoiceGeneratorDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICustomFieldRepository, CustomFieldRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddScoped<IInvoiceNumberGenerator, InvoiceNumberGenerator>();

            // QuestPDF Community license: free for individuals and companies under $1M annual revenue
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            services.AddSingleton<IInvoicePdfGenerator, InvoicePdfGenerator>();

            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            return services;
        }
    }
}
