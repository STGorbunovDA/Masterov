using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

public class GetComponentTypesByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypesByUpdatedAtStorage
{
    public async Task<IEnumerable<ComponentTypeDomain>?> GetComponentTypesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var componentTypes = await dbContext.ComponentTypes
            .AsNoTracking()
            .Where(ct => ct.UpdatedAt >= startOfDay && ct.UpdatedAt < endOfDay)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ComponentTypeDomain>>(componentTypes);
    }
}