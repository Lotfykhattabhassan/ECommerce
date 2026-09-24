namespace MiniECommerce.Modules.Inventory.Application.DTOs
{
    public record ReserveStockDto(Guid ProductId, int Quantity);
}
