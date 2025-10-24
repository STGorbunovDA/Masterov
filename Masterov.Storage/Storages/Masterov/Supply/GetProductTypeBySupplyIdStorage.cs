using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetProductTypeBySupplyIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductTypeBySupplyIdStorage
{
    public async Task<ComponentTypeDomain?> GetProductTypeBySupplyId(Guid supplyId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetProductTypeBySupplyId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.Supplies
                    .AsNoTracking() 
                    .Where(o => o.SupplyId == supplyId)
                        .Select(o => o.ComponentType)
                    .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            });
}