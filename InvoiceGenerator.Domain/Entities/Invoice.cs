namespace InvoiceGenerator.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = default!;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public int CustomerId { get; set; }


        // Computed properties
        public decimal SubTotal
        {
            get
            {
                return InvoiceItems.Sum(item => item.Quantity * item.UnitPrice);
            }
        }
        public decimal SubTotalAfterDiscount
        {
            get
            {
                return SubTotal - DiscountAmount;
            }
        }
        public decimal TaxAmount
        {
            get
            {
                return SubTotalAfterDiscount * (TaxPercentage / 100);
            }
        }
        public decimal Total
        {
            get
            {
                return SubTotalAfterDiscount + TaxAmount;
            }
        }


        // Navigation properties
        public Customer Customer { get; set; } = default!;
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        public ICollection<InvoiceCustomField> InvoiceCustomFields { get; set; } = new List<InvoiceCustomField>();
    }
}
