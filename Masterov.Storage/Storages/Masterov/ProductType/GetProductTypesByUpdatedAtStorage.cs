using AutoMapper;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByUpdatedAt;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductType;

public class GetProductTypesByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetProductTypesByUpdatedAtStorage
{
    public async Task<IEnumerable<ProductTypeDomain>?> GetProductTypesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var componentTypes = await dbContext.ProductTypes
            .AsNoTracking()
            .Where(ct => ct.UpdatedAt >= startOfDay && ct.UpdatedAt < endOfDay)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ProductTypeDomain>>(componentTypes);
    }
}