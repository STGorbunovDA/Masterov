using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.GetUsedComponentsByComponentTypeId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

public class GetUsedComponentsByComponentTypeIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentsByComponentTypeIdStorage
{
    public async Task<IEnumerable<UsedComponentDomain>?> GetUsedComponentsByComponentTypeId(Guid componentTypeId, CancellationToken cancellationToken)
    {
        var usedComponents = await dbContext.UsedComponents
            .AsNoTracking()
            .Where(u => u.ComponentTypeId == componentTypeId)
                .Include(u => u.ComponentType)
                .Include(u => u.Warehouse)
                .Include(u => u.Order)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UsedComponentDomain>>(usedComponents);
    }
}