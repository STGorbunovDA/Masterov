using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class GetComponentTypeByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetComponentTypeByIdStorage
{
    public async Task<ComponentTypeDomain?> GetComponentTypeById(Guid productTypeId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ComponentTypeDomain?>( 
            nameof(GetComponentTypeById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ComponentTypes
                    .AsNoTracking() 
                    .Where(f => f.ComponentTypeId == productTypeId)
                    .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}