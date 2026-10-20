using InvoiceGenerator.Common.Enums;

namespace InvoiceGenerator.Domain.Entities
{
    public class CustomField
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public CustomFieldType Type { get; set; } = default!;
        public bool Enabled { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
