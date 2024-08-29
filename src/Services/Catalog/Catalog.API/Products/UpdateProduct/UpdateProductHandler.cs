﻿namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,
    string Name,
    string Description,
    List<string> Category,
    string ImageFile,
    decimal Price):ICommand<UpdateProductCommandResult>;

public record UpdateProductCommandResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required"); 
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater then 0");
    }
}
internal class UpdateProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        session.Update(product);
        await session.SaveChangesAsync();

        return new UpdateProductCommandResult(true);

    }
}