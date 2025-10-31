using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class UpdateUsedComponentStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateUsedComponentStorage
{
    public async Task<UsedComponentDomain> UpdateUsedComponent(Guid usedComponentId, Guid orderId, Guid componentTypeId, Guid warehouseId, int quantity,
        DateTime? createdAt, CancellationToken cancellationToken)
    {
        var usedComponentExists = await dbContext.Set<Storage.UsedComponent>().FindAsync([usedComponentId], cancellationToken);
        
        if (usedComponentExists == null)
            throw new Exception("usedComponent not found");

        usedComponentExists.OrderId = orderId;
        usedComponentExists.ComponentTypeId = componentTypeId;
        usedComponentExists.WarehouseId = warehouseId;
        usedComponentExists.Quantity = quantity;
        if (createdAt.HasValue)
            usedComponentExists.CreatedAt = createdAt.Value;
        usedComponentExists.UpdatedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var usedComponent = await dbContext.UsedComponents
            .AsNoTracking()
            .Where(f => f.UsedComponentId == usedComponentId)
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ComponentType)
            .FirstOrDefaultAsync(cancellationToken);
        
        return mapper.Map<UsedComponentDomain>(usedComponent);
    }
}