

namespace MiniECommerce.Modules.Catalog.Application.Exceptions.Product
{
    public class ProductSkuAlreadyExistsException : Exception
    {
        public ProductSkuAlreadyExistsException()
            : base ("Product Sku Already Exists Exception")
        {
            
        }
    }
}
