using BuildingBlocks.Exceptions;
using System.Net;

namespace Catalog.Api.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public HttpStatusCode StatusCode { get; }
    public ProductNotFoundException(Guid id) : base("Product",id)
    {
        StatusCode = HttpStatusCode.NotFound;
     }

}


