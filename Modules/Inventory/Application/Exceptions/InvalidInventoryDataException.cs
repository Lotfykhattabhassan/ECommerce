namespace MiniECommerce.Modules.Inventory.Application.Exceptions
{
    public class InvalidInventoryDataException : Exception
    {
        public InvalidInventoryDataException(string message = "Invalid inventory data.") : base(message) { }
    }
}
