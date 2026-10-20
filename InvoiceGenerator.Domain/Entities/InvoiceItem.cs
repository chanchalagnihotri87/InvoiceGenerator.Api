namespace InvoiceGenerator.Domain.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        //Computed Properties
        public decimal TotalPrice
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }


        // Navigation properties
        public Item Item { get; set; } = default!;
        public Invoice Invoice { get; set; } = default!;
    }
}
