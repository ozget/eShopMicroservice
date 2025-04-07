namespace Catalog.Api.Products.GetProductById
{
    public record GetProductByIdResponse(Product Product);//yanıt olarak geri dönecek

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));//nesnesini göndermek icin mediatR handler tetikleyecek

                var response = result.Adapt<GetProductByIdResponse>();// bu yanıt nesnesi ile client geri döndürüyoruz

                return Results.Ok(response);
            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product By Id")
                .WithDescription("Get Product By Id");
        }
    }

}
