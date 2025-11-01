using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponents;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class GetUsedComponentsStorage(MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentsStorage
{
    public async Task<IEnumerable<UsedComponentDomain?>> GetUsedComponents(CancellationToken cancellationToken)
    {
        var usedComponents = await dbContext.UsedComponents
            .AsNoTracking()
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ComponentType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UsedComponentDomain>>(usedComponents);
    }
}