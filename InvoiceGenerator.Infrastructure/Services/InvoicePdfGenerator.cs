using System.Globalization;
using InvoiceGenerator.Application.Interfaces;
using InvoiceGenerator.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvoiceGenerator.Infrastructure.Services
{
    public class InvoicePdfGenerator : IInvoicePdfGenerator
    {
        public byte[] Generate(Invoice invoice)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(c => ComposeHeader(c, invoice));
                    page.Content().PaddingVertical(20).Element(c => ComposeContent(c, invoice));
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Page ");
                        text.CurrentPageNumber();
                        text.Span(" of ");
                        text.TotalPages();
                    });
                });
            }).GeneratePdf();
        }

        private static void ComposeHeader(IContainer container, Invoice invoice)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("INVOICE").FontSize(24).Bold().FontColor(Colors.Blue.Darken2);
                    column.Item().Text($"Invoice #: {invoice.InvoiceNumber}").SemiBold();
                    column.Item().Text($"Invoice date: {FormatDate(invoice.InvoiceDate)}");
                    column.Item().Text($"Due date: {FormatDate(invoice.DueDate)}");
                });

                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item().Text("Bill to").SemiBold().FontColor(Colors.Grey.Darken1);
                    column.Item().Text(invoice.Customer.Name).Bold();
                    column.Item().Text(invoice.Customer.Address);
                    column.Item().Text(invoice.Customer.Email);
                    if (!string.IsNullOrWhiteSpace(invoice.Customer.Phone))
                    {
                        column.Item().Text(invoice.Customer.Phone);
                    }
                });
            });
        }

        private static void ComposeContent(IContainer container, Invoice invoice)
        {
            container.Column(column =>
            {
                column.Spacing(15);

                column.Item().Element(c => ComposeItemsTable(c, invoice));
                column.Item().AlignRight().Width(220).Element(c => ComposeTotals(c, invoice));

                if (invoice.InvoiceCustomFields.Count > 0)
                {
                    column.Item().Element(c => ComposeCustomFields(c, invoice));
                }
            });
        }

        private static void ComposeItemsTable(IContainer container, Invoice invoice)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCell).Text("#");
                    header.Cell().Element(HeaderCell).Text("Item");
                    header.Cell().Element(HeaderCell).AlignRight().Text("Qty");
                    header.Cell().Element(HeaderCell).AlignRight().Text("Unit price");
                    header.Cell().Element(HeaderCell).AlignRight().Text("Total");
                });

                var number = 1;
                foreach (var line in invoice.InvoiceItems)
                {
                    table.Cell().Element(BodyCell).Text($"{number++}");
                    table.Cell().Element(BodyCell).Column(c =>
                    {
                        c.Item().Text(line.Item.Name);
                        if (!string.IsNullOrWhiteSpace(line.Item.Description))
                        {
                            c.Item().Text(line.Item.Description).FontSize(8).FontColor(Colors.Grey.Darken1);
                        }
                    });
                    table.Cell().Element(BodyCell).AlignRight().Text($"{line.Quantity}");
                    table.Cell().Element(BodyCell).AlignRight().Text(FormatAmount(line.UnitPrice));
                    table.Cell().Element(BodyCell).AlignRight().Text(FormatAmount(line.TotalPrice));
                }
            });

            static IContainer HeaderCell(IContainer c) =>
                c.BorderBottom(1).BorderColor(Colors.Grey.Medium).PaddingVertical(5).DefaultTextStyle(x => x.SemiBold());

            static IContainer BodyCell(IContainer c) =>
                c.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
        }

        private static void ComposeTotals(IContainer container, Invoice invoice)
        {
            container.Column(column =>
            {
                AddTotalRow(column, "Subtotal", FormatAmount(invoice.SubTotal));
                if (invoice.DiscountAmount > 0)
                {
                    AddTotalRow(column, "Discount", $"-{FormatAmount(invoice.DiscountAmount)}");
                }
                AddTotalRow(column, $"Tax ({invoice.TaxPercentage.ToString("0.##", CultureInfo.InvariantCulture)}%)", FormatAmount(invoice.TaxAmount));

                column.Item().BorderTop(1).BorderColor(Colors.Grey.Medium).PaddingTop(5).Row(row =>
                {
                    row.RelativeItem().Text("Total").Bold().FontSize(12);
                    row.RelativeItem().AlignRight().Text(FormatAmount(invoice.Total)).Bold().FontSize(12);
                });
            });

            static void AddTotalRow(ColumnDescriptor column, string label, string value)
            {
                column.Item().PaddingBottom(3).Row(row =>
                {
                    row.RelativeItem().Text(label);
                    row.RelativeItem().AlignRight().Text(value);
                });
            }
        }

        private static void ComposeCustomFields(IContainer container, Invoice invoice)
        {
            container.Column(column =>
            {
                column.Item().Text("Additional information").SemiBold();
                foreach (var field in invoice.InvoiceCustomFields)
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"{field.CustomField.Name}: ").SemiBold();
                        text.Span(field.Value);
                    });
                }
            });
        }

        private static string FormatAmount(decimal amount) => amount.ToString("N2", CultureInfo.InvariantCulture);

        private static string FormatDate(DateTime date) => date.ToString("dd MMM yyyy", CultureInfo.InvariantCulture);
    }
}
