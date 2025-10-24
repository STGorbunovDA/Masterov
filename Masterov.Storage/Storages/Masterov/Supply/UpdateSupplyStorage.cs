using AutoMapper;
using Masterov.Domain.Masterov.Supply.UpdateSupply;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class UpdateSupplyStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateSupplyStorage
{
    public async Task<SupplyDomain> UpdateSupply(Guid supplyId, Guid supplierId, Guid productTypeId, Guid warehouseId, int quantity, decimal priceSupply, CancellationToken cancellationToken)
    {
        var supplyExists = await dbContext.Set<Storage.Supply>().FindAsync([supplyId], cancellationToken);
        
        if (supplyExists == null)
            throw new Exception("supply not found");
        
        supplyExists.SupplierId = supplierId;
        supplyExists.ProductTypeId = productTypeId;
        supplyExists.WarehouseId = warehouseId;
        supplyExists.Quantity = quantity;
        supplyExists.PriceSupply = priceSupply;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var supply = await dbContext.Supplies
            .AsNoTracking()
            .Include(c => c.ComponentType)
            .Include(o => o.Warehouse) 
                .ThenInclude(w => w.ComponentType)
            .Include(c => c.Supplier)
            .Where(f => f.SupplyId == supplyId)
            .FirstOrDefaultAsync( cancellationToken);

        return mapper.Map<SupplyDomain>(supply);
    }
}