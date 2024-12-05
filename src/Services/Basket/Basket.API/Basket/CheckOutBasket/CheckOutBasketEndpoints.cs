namespace Basket.API.Basket.CheckOutBasket;
public record CheckOutBasketRequest(CheckOutBasketDto CheckOutBasketDto);
public record CheckOutBasketResponse(bool IsSuccess);
public class CheckOutBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckOutBasketRequest request,ISender sender) =>
        {
            var command = request.Adapt<CheckOutBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CheckOutBasketResponse>();
            return Results.Ok(response);
        }).WithName("CheckoutBasket")
        .Produces<CheckOutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Checkout Basket")
        .WithSummary("Checkout Basket");
    }
}
