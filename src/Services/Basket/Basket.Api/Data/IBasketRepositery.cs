using Basket.Api.Models;

namespace Basket.Api.Data;

public interface IBasketRepositery
{
    Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default);  
    Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
}
