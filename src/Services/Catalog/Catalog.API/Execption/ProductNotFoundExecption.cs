namespace Catalog.API.Execption
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid Id)
            :base($"The product id {Id} not found !")
        {
                
        }
    }
}
