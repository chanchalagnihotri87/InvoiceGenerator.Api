namespace InvoiceGenerator.Application.Features.Invoices.Dtos
{
    // Fields shared by CreateInvoiceRequest and UpdateInvoiceRequest, so both use one validator
    public interface IInvoiceInput
    {
        DateTime InvoiceDate { get; }
        DateTime DueDate { get; }
        int CustomerId { get; }
        decimal DiscountAmount { get; }
        decimal TaxPercentage { get; }
        IReadOnlyList<InvoiceItemInput> Items { get; }
        IReadOnlyList<InvoiceCustomFieldInput>? CustomFields { get; }
    }

    // UnitPrice is optional; when omitted the item's current price is used
    public record InvoiceItemInput(int ItemId, int Quantity, decimal? UnitPrice);

    public record InvoiceCustomFieldInput(int CustomFieldId, string Value);
}
