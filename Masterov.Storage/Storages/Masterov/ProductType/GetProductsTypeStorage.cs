using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductType.GetProductsType;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class GetProductsTypeStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductsTypeStorage
{
    public async Task<IEnumerable<ProductTypeDomain>> GetProductsType(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ProductTypeDomain[]>(
            nameof(GetProductsType),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ProductTypes
                    .AsNoTracking() 
                    .ProjectTo<ProductTypeDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}