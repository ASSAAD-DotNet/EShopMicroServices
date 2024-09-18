using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckOutBasket;
public sealed record CheckOutBasketCommand(CheckOutBasketDto CheckOutBasketDto): ICommand<CheckOutBasketResult>;
public sealed record CheckOutBasketResult(bool IsSuccess);

public sealed class CheckOutBasketCommandValidator :
    AbstractValidator<CheckOutBasketCommand>
{
    public CheckOutBasketCommandValidator()
    {
        RuleFor(x => x.CheckOutBasketDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
        RuleFor(x => x.CheckOutBasketDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public sealed class CheckOutBasketHandler
     (IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckOutBasketCommand, CheckOutBasketResult>
{
    public async Task<CheckOutBasketResult> Handle(CheckOutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // Set totalprice on basketcheckout event message
        // send basket checkout event to rabbitmq using masstransit
        // delete the basket
        var basket = await repository.GetBasket(command.CheckOutBasketDto.UserName, cancellationToken);
        if (basket == null)
        {
            return new CheckOutBasketResult(false);
        }

        var eventMessage = command.CheckOutBasketDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(command.CheckOutBasketDto.UserName, cancellationToken);

        return new CheckOutBasketResult(true);
    }
}