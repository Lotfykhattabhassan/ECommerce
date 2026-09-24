namespace MiniECommerce.Modules.Inventory.Application.DTOs
{
    public record CreateInventoryDto(Guid ProductId, int Quantity);
}
