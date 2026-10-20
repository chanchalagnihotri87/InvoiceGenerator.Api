namespace InvoiceGenerator.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
