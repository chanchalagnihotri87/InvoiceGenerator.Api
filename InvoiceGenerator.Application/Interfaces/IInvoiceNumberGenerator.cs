namespace InvoiceGenerator.Application.Interfaces
{
    public interface IInvoiceNumberGenerator
    {
        // Returns a new, unique invoice number; safe to call concurrently
        Task<string> GenerateAsync(CancellationToken cancellationToken = default);
    }
}
