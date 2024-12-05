namespace Catalog.API.Execption;
public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id)
        :base(nameof(Product),Id)
    {      
    }
}