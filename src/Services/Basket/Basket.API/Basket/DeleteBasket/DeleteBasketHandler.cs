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

internal class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        return new DeleteBasketResult(true);
    }
}