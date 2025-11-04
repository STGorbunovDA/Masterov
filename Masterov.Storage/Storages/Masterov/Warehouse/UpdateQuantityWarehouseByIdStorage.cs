using AutoMapper;
using Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class UpdateQuantityWarehouseByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateQuantityWarehouseByIdStorage
{
    public async Task<WarehouseDomain> UpdateQuantityWarehouseById(Guid warehouseId, int quantity, CancellationToken cancellationToken)
    {
        var warehouseExists = await dbContext.Set<Storage.Warehouse>().FindAsync([warehouseId], cancellationToken);
        
        if (warehouseExists == null)
            throw new Exception("warehouse not found");
        warehouseExists.Quantity = quantity;
        warehouseExists.UpdatedAt = DateTime.Now;

        if (warehouseExists.Quantity == 0)
            warehouseExists.Price = 0;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var warehouse = await dbContext.Warehouses
            .AsNoTracking()
                .Include(c => c.ComponentType)
            .Where(f => f.WarehouseId == warehouseId)
            .FirstOrDefaultAsync( cancellationToken);

        return mapper.Map<WarehouseDomain>(warehouse);
    }
}