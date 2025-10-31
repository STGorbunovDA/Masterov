using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class GetComponentTypeByUsedComponentIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypeByUsedComponentIdStorage
{
    public async Task<ComponentTypeDomain?> GetComponentTypeByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken) =>
        await dbContext.UsedComponents
            .AsNoTracking() 
            .Where(o => o.UsedComponentId == usedComponentId)
            .Select(o => o.ComponentType)
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}