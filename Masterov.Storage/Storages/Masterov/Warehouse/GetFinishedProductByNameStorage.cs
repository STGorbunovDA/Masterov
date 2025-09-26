using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetWarehouseByNameStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetWarehouseByNameStorage
{
    public async Task<IEnumerable<WarehouseDomain?>> GetWarehouseByName(
        string name,
        CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            $"{nameof(GetWarehouseByName)}_{name.ToLower().Trim()}",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return await dbContext.Warehouses
                    .AsNoTracking()
                    .Where(f => f.Name.ToLower().Contains(name.ToLower().Trim()))
                    .ProjectTo<WarehouseDomain>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }) ?? Enumerable.Empty<WarehouseDomain>();
}
            