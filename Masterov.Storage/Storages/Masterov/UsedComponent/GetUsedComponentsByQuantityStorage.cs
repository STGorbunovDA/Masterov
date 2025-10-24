using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentsByQuantity;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class GetUsedComponentsByQuantityStorage(MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentsByQuantityStorage
{
    public async Task<IEnumerable<UsedComponentDomain?>> GetUsedComponentsByQuantity(int quantity, CancellationToken cancellationToken)
    {
       
        var supplies = await dbContext.UsedComponents
            .AsNoTracking() 
            .Where(p => p.Quantity == quantity)
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ComponentType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<UsedComponentDomain>>(supplies);
    }
}