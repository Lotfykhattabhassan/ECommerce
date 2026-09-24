namespace MiniECommerce.Modules.Inventory.Application.DTOs
{
    public record UpdateStockDto(Guid ProductId, int Quantity);
}
