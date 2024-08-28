namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler
    (IDocumentSession session)
    :ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logique to create Product
        var product = new Product()
        {
            Name = command.Name,
            Description = command.Description,
            Category = command.Category,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        //Save to the data base
        session.Store(product);
        await session.SaveChangesAsync();

        //Return result
        return new CreateProductResult(product.Id);
    }
}