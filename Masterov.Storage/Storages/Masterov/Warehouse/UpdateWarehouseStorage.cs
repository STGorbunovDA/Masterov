using AutoMapper;
using Masterov.Domain.Masterov.Warehouse.UpdateWarehouse;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class UpdateWarehouseStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateWarehouseStorage
{
    public async Task<WarehouseDomain> UpdateWarehouse(Guid warehouseId, Guid componentTypeId, string name, int quantity, decimal price, DateTime? createdAt,
        CancellationToken cancellationToken)
    {
        var warehouseExists = await dbContext.Set<Storage.Warehouse>().FindAsync([warehouseId], cancellationToken);
        
        if (warehouseExists == null)
            throw new Exception("warehouse not found");
        
        warehouseExists.ComponentTypeId = componentTypeId;
        warehouseExists.Name = name;
        warehouseExists.Quantity = quantity;
        warehouseExists.Price = price;
        
        if (createdAt.HasValue)
            warehouseExists.CreatedAt = createdAt.Value;
        
        warehouseExists.UpdatedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var warehouse = await dbContext.Warehouses
            .AsNoTracking()
                .Include(c => c.ComponentType)
            .Where(f => f.WarehouseId == warehouseId)
            .FirstOrDefaultAsync( cancellationToken);

        return mapper.Map<WarehouseDomain>(warehouse);
    }
}