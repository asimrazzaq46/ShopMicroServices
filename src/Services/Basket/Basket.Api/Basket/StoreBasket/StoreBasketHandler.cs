using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using FluentValidation;


namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("UserName cannot be null");
        
    }
}

public class StoreBasketCommandHandler(IBasketRepositery repo) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        // store basket in the database(use Marten upsert - if exist = Update else Create)
        await repo.StoreBasketAsync(cart,cancellationToken);


        return new StoreBasketResult(cart.UserName);
    }
}
