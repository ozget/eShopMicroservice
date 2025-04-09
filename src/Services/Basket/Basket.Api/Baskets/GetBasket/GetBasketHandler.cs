

namespace Basket.Api.Baskets.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResualt>;
    public record GetBasketResualt(ShoppingCart Cart);



    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResualt>
    {
        public async Task<GetBasketResualt> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
