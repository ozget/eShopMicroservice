namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException:Exception
    {
        public ProductNotFoundException(Guid Id):base("Product not foud! {@Id}")
        {
                
        }
    }
}
