

using Catalog.Api.Products.GetProduct;

namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Category,string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);



    public class CreateProductCommadValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommadValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                                .Length(2, 150).WithMessage("Name must be 2 and 150 characters");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }




    //CreateProductCommand, CreateProductResult tetiklenmesi icin  : ICommandHandler şeklinde yazdık
    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateProductCommandHandler {@Command}", command);

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);// product sessionda saklıyoruz
            await session.SaveChangesAsync(cancellationToken);//postgreSql kaydetme
            return new CreateProductResult(product.Id);
        }
    }
}
