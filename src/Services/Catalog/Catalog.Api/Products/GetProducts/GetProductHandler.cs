
namespace Catalog.Api.Products.GetProducts;

public record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);
internal class GetProductHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        // Find product entity with help of Marten Library
        var productsResult = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1,query.PageSize ?? 10 ,cancellationToken);

        return new GetProductResult(productsResult);

    }
}

