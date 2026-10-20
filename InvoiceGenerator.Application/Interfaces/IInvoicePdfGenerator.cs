using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Interfaces
{
    public interface IInvoicePdfGenerator
    {
        // Expects Customer, InvoiceItems.Item and InvoiceCustomFields.CustomField to be loaded
        byte[] Generate(Invoice invoice);
    }
}
