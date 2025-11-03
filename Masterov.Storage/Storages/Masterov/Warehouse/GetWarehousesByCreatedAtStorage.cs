using AutoMapper;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

public class GetWarehousesByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetWarehousesByCreatedAtStorage
{
    public async Task<IEnumerable<WarehouseDomain>?> GetWarehousesByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var warehouses = await dbContext.Warehouses
            .AsNoTracking() 
            .Where(s => s.CreatedAt >= startOfDay && s.CreatedAt < endOfDay)
                .Include(c => c.ComponentType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<WarehouseDomain>>(warehouses);
    }
}