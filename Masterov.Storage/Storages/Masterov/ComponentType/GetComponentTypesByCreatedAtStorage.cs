using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ComponentType;

public class GetComponentTypesByCreatedAtStorage(MasterovDbContext dbContext, IMapper mapper)
    : IGetComponentTypesByCreatedAtStorage
{
    public async Task<IEnumerable<ComponentTypeDomain>?> GetComponentTypesByCreatedAt(
        DateTime? createdAt,
        CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;

        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var componentTypes = await dbContext.ComponentTypes
            .AsNoTracking()
            .Where(ct => ct.CreatedAt >= startOfDay && ct.CreatedAt < endOfDay)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ComponentTypeDomain>>(componentTypes);
    }
}