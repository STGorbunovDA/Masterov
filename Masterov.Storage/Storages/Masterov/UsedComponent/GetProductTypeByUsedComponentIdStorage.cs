using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.UsedComponent.GetProductTypeByUsedComponentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class GetProductTypeByUsedComponentIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetProductTypeByUsedComponentIdStorage
{
    public async Task<ComponentTypeDomain?> GetProductTypeByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken) =>
        await dbContext.UsedComponents
            .AsNoTracking() 
            .Where(o => o.UsedComponentId == usedComponentId)
            .Select(o => o.ComponentType)
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}