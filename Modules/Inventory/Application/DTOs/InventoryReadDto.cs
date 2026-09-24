namespace MiniECommerce.Modules.Inventory.Application.DTOs
{
    public record InventoryReadDto(
        Guid Id,
        Guid ProductId,
        int Quantity,
        int ReservedQuantity,
        int AvailableQuantity);
}
