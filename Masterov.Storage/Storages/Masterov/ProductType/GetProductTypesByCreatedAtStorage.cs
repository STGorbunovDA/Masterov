using AutoMapper;
using Masterov.Domain.Masterov.ProductType.GetProductTypesByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductType;

public class GetProductTypesByCreatedAtStorage(MasterovDbContext dbContext, IMapper mapper)
    : IGetProductTypesByCreatedAtStorage
{
    public async Task<IEnumerable<ProductTypeDomain>?> GetProductTypesByCreatedAt(
        DateTime? createdAt,
        CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;

        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var componentTypes = await dbContext.ProductTypes
            .AsNoTracking()
            .Where(ct => ct.CreatedAt >= startOfDay && ct.CreatedAt < endOfDay)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ProductTypeDomain>>(componentTypes);
    }
}