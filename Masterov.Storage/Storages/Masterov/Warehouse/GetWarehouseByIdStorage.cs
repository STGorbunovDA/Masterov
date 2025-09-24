using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetWarehouseByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetWarehouseByIdStorage
{
    public async Task<WarehouseDomain?> GetWarehouseById(Guid warehouseById, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<WarehouseDomain?>( 
            nameof(GetWarehouseById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Warehouses
                    .AsNoTracking() 
                    .Where(f => f.WarehouseId == warehouseById)
                    .ProjectTo<WarehouseDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}