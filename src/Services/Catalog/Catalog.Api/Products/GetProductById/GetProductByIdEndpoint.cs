﻿
using Catalog.Api.Exceptions;
using Catalog.Api.Products.GetProducts;

namespace Catalog.Api.Products.GetProductById;


public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async(Guid id,ISender sender) =>
        {
            
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
          
        })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Products by id")
            .WithDescription("Get Product by Id");
    }
}
