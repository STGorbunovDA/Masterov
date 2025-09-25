using AutoMapper;
using Masterov.Domain.Masterov.Warehouse.GetWarehouses;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetWarehousesStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetWarehousesStorage
{
    public async Task<IEnumerable<WarehouseDomain>> GetWarehouses(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(
            nameof(GetWarehouses),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var supplies = await dbContext.Warehouses
                    .AsNoTracking()
                        .Include(c => c.ProductType)
                    .ToArrayAsync(cancellationToken);

                return mapper.Map<WarehouseDomain[]>(supplies); 
            }))!;
}