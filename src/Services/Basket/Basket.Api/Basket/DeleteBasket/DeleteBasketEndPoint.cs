﻿using Basket.Api.Basket.GetBasket;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
            
        })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket");

    }
}
