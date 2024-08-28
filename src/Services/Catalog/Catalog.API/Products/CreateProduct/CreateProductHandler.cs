namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidaitor 
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidaitor()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("The price must be greater then 0");
    }
}

internal class CreateProductCommandHandler
    (IDocumentSession session,IValidator<CreateProductCommand> validator)
    :ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validate = await validator.ValidateAsync(command);
        var errors = validate.Errors.Select(x => x.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }
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