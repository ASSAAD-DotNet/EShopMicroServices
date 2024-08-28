using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductById;

//public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{Id:guid}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);

        }).WithDisplayName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product by Id")
        .WithDescription("Get Product by Id"); ;
    }
}