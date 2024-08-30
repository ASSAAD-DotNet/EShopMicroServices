namespace Basket.API.Basket.StoreBasket;
public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(bool IsSuccess);
public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var stroreBasketCommand = request.Adapt<StoreBasketCommand>();
            var result = await sender.Send(stroreBasketCommand);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Ok(response);

        }).WithDisplayName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Basket")
        .WithDescription("Store Basket");
    }
}