using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypes;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class GetComponentTypesStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductsTypeStorage
{
    public async Task<IEnumerable<ComponentTypeDomain>> GetComponentTypes(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ComponentTypeDomain[]>(
            nameof(GetComponentTypes),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ComponentTypes
                    .AsNoTracking() 
                    .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}