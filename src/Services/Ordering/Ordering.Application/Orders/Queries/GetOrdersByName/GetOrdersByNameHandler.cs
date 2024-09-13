namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await dbContext.Orders
                    .Include(o => o.OrderItems)
                    .AsNoTracking()
                    .Where(o => o.OrderName.Value.Contains(query.OrderName))
                    .OrderBy(o => o.OrderName.Value)
                    .ToListAsync();

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
        catch (Exception ex)
        {

            throw ex;
        }
        
    }
}
