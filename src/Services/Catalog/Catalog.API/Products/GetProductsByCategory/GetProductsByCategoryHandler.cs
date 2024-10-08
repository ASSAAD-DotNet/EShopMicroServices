﻿namespace Catalog.API.Products.GetProductsByCategory;
public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductsByCategoryQueryHandler 
    (IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger)
    :IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetProductsByCategoryQueryHandler.Handle call with {query}");
        
        var products = await session.Query<Product>()
            .Where(p=> p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}