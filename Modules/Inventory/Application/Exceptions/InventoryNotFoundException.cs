namespace MiniECommerce.Modules.Inventory.Application.Exceptions
{
    public class InventoryNotFoundException : Exception
    {
        public InventoryNotFoundException(string message = "Inventory was not found.") : base(message) { }
    }
}
