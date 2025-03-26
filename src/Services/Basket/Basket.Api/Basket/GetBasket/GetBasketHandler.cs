using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Marten;

namespace Basket.Api.Basket.GetBasket;



public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBaskeQuerytHandler(IDocumentSession session ) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {

        // TODO Get basket from the database
        //    var basket = await _repo.GetBasketAsync(query.UserName);
        //

        return new GetBasketResult(new ShoppingCart("it's a test, implement the feature"));
    }
}

