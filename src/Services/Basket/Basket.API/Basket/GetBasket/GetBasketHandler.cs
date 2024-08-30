namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) :IQuery<GetbasketResult>;
public record GetbasketResult(ShoppingCart Cart);

internal class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetbasketResult>
{
    public async Task<GetbasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        return new GetbasketResult(new ShoppingCart("assaad"));
    }
}

