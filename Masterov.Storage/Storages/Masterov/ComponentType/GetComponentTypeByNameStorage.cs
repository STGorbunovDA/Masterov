using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

internal class GetComponentTypeByNameStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetComponentTypeByNameStorage
{
    public async Task<ComponentTypeDomain?> GetComponentTypeByName(string productTypeName, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ComponentTypeDomain?>( 
            nameof(GetComponentTypeByName),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ComponentTypes
                    .AsNoTracking() 
                    .Where(f => f.Name.ToLower() == productTypeName.ToLower().Trim())
                    .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}