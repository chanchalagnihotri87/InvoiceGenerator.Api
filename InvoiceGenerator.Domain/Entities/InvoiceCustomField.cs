namespace InvoiceGenerator.Domain.Entities
{
    public class InvoiceCustomField
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int CustomFieldId { get; set; }
        public string Value { get; set; } = string.Empty;
        public Invoice Invoice { get; set; } = null!;
        public CustomField CustomField { get; set; } = null!;
    }
}
