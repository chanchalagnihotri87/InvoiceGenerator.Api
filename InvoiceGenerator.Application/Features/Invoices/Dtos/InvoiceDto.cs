using InvoiceGenerator.Common.Enums;
using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Features.Invoices.Dtos
{
    public record InvoiceDto(
        int Id,
        string InvoiceNumber,
        DateTime InvoiceDate,
        DateTime DueDate,
        int CustomerId,
        string CustomerName,
        decimal DiscountAmount,
        decimal TaxPercentage,
        decimal SubTotal,
        decimal SubTotalAfterDiscount,
        decimal TaxAmount,
        decimal Total,
        IReadOnlyList<InvoiceItemDto> Items,
        IReadOnlyList<InvoiceCustomFieldDto> CustomFields)
    {
        // Expects Customer, InvoiceItems.Item and InvoiceCustomFields.CustomField to be loaded
        public static InvoiceDto FromEntity(Invoice invoice) =>
            new(
                invoice.Id,
                invoice.InvoiceNumber,
                invoice.InvoiceDate,
                invoice.DueDate,
                invoice.CustomerId,
                invoice.Customer.Name,
                invoice.DiscountAmount,
                invoice.TaxPercentage,
                invoice.SubTotal,
                invoice.SubTotalAfterDiscount,
                invoice.TaxAmount,
                invoice.Total,
                invoice.InvoiceItems.Select(InvoiceItemDto.FromEntity).ToList(),
                invoice.InvoiceCustomFields.Select(InvoiceCustomFieldDto.FromEntity).ToList());
    }

    public record InvoiceItemDto(int Id, int ItemId, string ItemName, int Quantity, decimal UnitPrice, decimal TotalPrice)
    {
        public static InvoiceItemDto FromEntity(InvoiceItem invoiceItem) =>
            new(invoiceItem.Id, invoiceItem.ItemId, invoiceItem.Item.Name, invoiceItem.Quantity, invoiceItem.UnitPrice, invoiceItem.TotalPrice);
    }

    public record InvoiceCustomFieldDto(int CustomFieldId, string Name, CustomFieldType Type, string Value)
    {
        public static InvoiceCustomFieldDto FromEntity(InvoiceCustomField invoiceCustomField) =>
            new(invoiceCustomField.CustomFieldId, invoiceCustomField.CustomField.Name, invoiceCustomField.CustomField.Type, invoiceCustomField.Value);
    }
}
