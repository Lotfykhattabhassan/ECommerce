namespace MiniECommerce.Modules.Inventory.Application.Exceptions
{
    public class InventoryAlreadyExistsException : Exception
    {
        public InventoryAlreadyExistsException(string message = "Inventory already exists for this product.") : base(message) { }
    }
}
