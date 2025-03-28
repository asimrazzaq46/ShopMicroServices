using BuildingBlocks.Exceptions;

namespace Basket.Api.Exception;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string username) : base("Basket" , username)
    {
    }
}
