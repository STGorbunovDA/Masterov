using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductType.GetProductTypeById;
using Masterov.Domain.Masterov.ProductType.GetProductTypeByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductType;

internal class GetProductTypeByNameStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductTypeByNameStorage
{
    public async Task<ProductTypeDomain?> GetProductTypeByName(string productTypeName, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ProductTypeDomain?>( 
            nameof(GetProductTypeByName),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ProductTypes
                    .AsNoTracking() 
                    .Where(f => f.Name.ToLower() == productTypeName.ToLower().Trim())
                    .ProjectTo<ProductTypeDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}