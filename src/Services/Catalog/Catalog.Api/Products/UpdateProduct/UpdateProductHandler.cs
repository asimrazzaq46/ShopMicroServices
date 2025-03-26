
namespace Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id,string? Name, List<string>? Category, string? Description,
                                    string? ImageFile, decimal? Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name Cannot be Empty")
            .Length(2,150).WithMessage("Name must be between 2 and 150 charectes");

        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id,cancellationToken);

        if (product == null) throw new ProductNotFoundException(command.Id);

        if(command.Name != null) product.Name = command.Name;
        if (command.Category != null) product.Category = command.Category;
        if (command.Description != null) product.Description = command.Description;
        if (command.ImageFile != null) product.ImageFile = command.ImageFile;
        if (command.Price.HasValue) product.Price = command.Price.Value;
    
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

         return new UpdateProductResult(true);


    }
}
