﻿namespace Catalog.Api.Products.GetProducts;

public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetProductResponse(IEnumerable<Product> Products);
public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapGet("/products",
            async ([AsParameters] GetProductRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProduct")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Products")
            .WithDescription("Get Product");
    }
}
