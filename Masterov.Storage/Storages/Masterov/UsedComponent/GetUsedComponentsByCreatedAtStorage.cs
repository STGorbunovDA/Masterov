using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

public class GetUsedComponentsByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentsByCreatedAtStorage
{
    public async Task<IEnumerable<UsedComponentDomain>?> GetUsedComponentsByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var usedComponents = await dbContext.UsedComponents
            .AsNoTracking() 
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ComponentType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UsedComponentDomain>>(usedComponents);
    }
}