using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Supply.GetWarehouseBySupplyId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetWarehouseBySupplyIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetWarehouseBySupplyIdStorage
{
    public async Task<WarehouseDomain?> GetWarehouseBySupplyId(Guid supplyId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetWarehouseBySupplyId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.Supplies
                    .AsNoTracking() 
                    .Where(o => o.SupplyId == supplyId)
                        .Include(o => o.Warehouse) 
                            .ThenInclude(w => w.ComponentType)
                    .Select(o => o.Warehouse)
                    .ProjectTo<WarehouseDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            });
}