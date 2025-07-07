using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class GetProductTypeByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductTypeByIdStorage
{
    public async Task<ProductTypeDomain?> GetProductTypeById(Guid productTypeId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ProductTypeDomain?>( 
            nameof(GetProductTypeById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ProductTypes
                    .AsNoTracking() 
                    .Where(f => f.ProductTypeId == productTypeId)
                    .ProjectTo<ProductTypeDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}