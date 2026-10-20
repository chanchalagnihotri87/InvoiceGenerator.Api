using InvoiceGenerator.Domain.Entities;

namespace InvoiceGenerator.Application.Features.Items.Dtos
{
    public record ItemDto(int Id, string Name, string? Description, decimal Price)
    {
        public static ItemDto FromEntity(Item item) =>
            new(item.Id, item.Name, item.Description, item.Price);
    }
}
