using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetSuppliesByWarehouseIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSuppliesByWarehouseIdStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetSuppliesByWarehouseId(Guid warehouseId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetSuppliesByWarehouseId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.Warehouses
                    .AsNoTracking()
                    .Where(o => o.WarehouseId == warehouseId)
                    .SelectMany(o => o.Supplies)
                    .ProjectTo<SupplyDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            });
}