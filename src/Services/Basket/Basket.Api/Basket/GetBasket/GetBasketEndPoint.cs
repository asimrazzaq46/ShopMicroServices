﻿using Basket.Api.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}",async(string username, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(username));

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);

        })
            .WithName("GetProductById")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
    }
}
