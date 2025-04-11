

namespace Basket.Api.Baskets.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResualt>;
    public record GetBasketResualt(ShoppingCart Cart);



    public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResualt>
    {
        public async Task<GetBasketResualt> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasketAsync(query.UserName);

            return  new GetBasketResualt(basket);
           
        }
    }
}
