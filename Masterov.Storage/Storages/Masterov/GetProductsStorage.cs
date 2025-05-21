using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.GetProducts;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov;

internal class GetProductsStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductsStorage
{
    public async Task<IEnumerable<ProductDomain>> GetProducts(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ProductDomain[]>(
            nameof(GetProducts),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Products
                    .ProjectTo<ProductDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}