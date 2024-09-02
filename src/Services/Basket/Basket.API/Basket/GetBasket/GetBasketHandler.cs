namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) :IQuery<GetbasketResult>;
public record GetbasketResult(ShoppingCart Cart);

internal class GetBasketQueryHandler
    (IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetbasketResult>
{
    public async Task<GetbasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var baskt = await repository.GetBasket(query.UserName, cancellationToken);
        return new GetbasketResult(baskt);
    }
}

