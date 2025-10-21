using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.GetComponents;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class UsedUsedComponentsStorage(MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentsStorage
{
    public async Task<IEnumerable<UsedComponentDomain>> GetUsedComponents(CancellationToken cancellationToken)
    {
        var usedComponents = await dbContext.UsedComponents
            .AsNoTracking()
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ProductType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UsedComponentDomain>>(usedComponents);
    }
}