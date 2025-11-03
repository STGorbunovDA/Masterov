using AutoMapper;
using Masterov.Domain.Masterov.Warehouse.GetWarehousesByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

public class GetWarehousesByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetWarehousesByUpdatedAtStorage
{
    public async Task<IEnumerable<WarehouseDomain>?> GetWarehousesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var warehouses = await dbContext.Warehouses
            .AsNoTracking() 
            .Where(s => s.UpdatedAt >= startOfDay && s.UpdatedAt < endOfDay)
                .Include(c => c.ComponentType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<WarehouseDomain>>(warehouses);
    }
}