
namespace Basket.Api.Data
{
    public class CachedBasketRepository(IBasketRepository repository) : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await repository.DeleteBasketAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await repository.GetBasketAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            return await repository.StoreBasketAsync(basket, cancellationToken);
        }
    }
}
