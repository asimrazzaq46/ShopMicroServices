

using BuildingBlocks.Behaviours;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name,List<string> Category , string Description,
                                    string ImageFile, decimal Price) :ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x=>x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("Image File is required");
        RuleFor(x=>x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class GetProductHandler(IDocumentSession session) :  ICommandHandler<CreateProductCommand, CreateProductResult> 
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        // Now this code is comented because
        // we are injecting service in Mediatr config program.cs ValidationBehaviour

        //var result = await validator.ValidateAsync(command, cancellationToken);
        //var errors = result.Errors.Select(x => x.ErrorMessage).ToList();


        // Create product entity from command object

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };
        //save database
        // TO-DO
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);


        //Return Result => CreateProductResult
        return new CreateProductResult(product.Id);
        
    }
}

