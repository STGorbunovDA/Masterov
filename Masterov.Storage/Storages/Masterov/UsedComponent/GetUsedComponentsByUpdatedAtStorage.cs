using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

public class GetUsedComponentsByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentsByUpdatedAtStorage
{
    public async Task<IEnumerable<UsedComponentDomain>?> GetUsedComponentsByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var usedComponents = await dbContext.UsedComponents
            .AsNoTracking() 
            .Where(order => order.UpdatedAt >= startOfDay && order.UpdatedAt < endOfDay)
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ProductType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UsedComponentDomain>>(usedComponents);
    }
}