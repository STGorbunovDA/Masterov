using AutoMapper;
using Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class UpdatePriceWarehouseByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdatePriceWarehouseByIdStorage
{
    public async Task<WarehouseDomain> UpdatePriceWarehouseById(Guid warehouseId, decimal price, CancellationToken cancellationToken)
    {
        var warehouseExists = await dbContext.Set<Storage.Warehouse>().FindAsync([warehouseId], cancellationToken);
        
        if (warehouseExists == null)
            throw new Exception("warehouse not found");
        
        warehouseExists.Price = price;
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