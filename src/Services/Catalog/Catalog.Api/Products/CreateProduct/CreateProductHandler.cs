using BuildingBlocks.Cqrs;
using Catalog.Api.Models;
using MediatR;

namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Category,string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    //CreateProductCommand, CreateProductResult tetiklenmesi icin  : ICommandHandler şeklinde yazdık
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };


            return new CreateProductResult(product.Id);
        }
    }
}
