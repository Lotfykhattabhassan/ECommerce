
namespace MiniECommerce.Modules.Catalog.Application.Exceptions.Product
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
            : base("Product Not Found")
        {
            
        }
    }
}
