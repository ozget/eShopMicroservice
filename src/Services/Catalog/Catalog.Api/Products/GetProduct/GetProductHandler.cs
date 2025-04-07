﻿using Marten.Pagination;

namespace Catalog.Api.Products.GetProduct
{
    public record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);

    internal class GetProductQueryHandler(IDocumentSession session,ILogger<GetProductQueryHandler> logger): IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductHandler {@@uery}", query);

            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetProductResult(products);
        }
    }
}
