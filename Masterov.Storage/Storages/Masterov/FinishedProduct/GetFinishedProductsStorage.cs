using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.FinishedProduct.GetProducts;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.FinishedProduct;

internal class GetFinishedProductsStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetFinishedProductsStorage
{
    public async Task<IEnumerable<FinishedProductDomain>> GetFinishedProducts(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<FinishedProductDomain[]>(
            nameof(GetFinishedProducts),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Products
                    .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}