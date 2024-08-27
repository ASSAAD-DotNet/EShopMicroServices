using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,
        string Description,
        List<string> Category,
        string ImageFile,
        decimal Price)
        : ICommand<CreateProductResult>;
    
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandHandler :
        ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //Business logique to create Product
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                ImageFile = request.ImageFile,
                Price = request.Price,
            };
            //Save to the data base

            //Return result
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
