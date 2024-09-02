namespace Basket.API.Basket.DeleteBasket;
public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandvalidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandvalidator()
    {
        RuleFor(x=> x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

internal class DeleteBasketCommandHandler 
    (IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(result);
    }
}