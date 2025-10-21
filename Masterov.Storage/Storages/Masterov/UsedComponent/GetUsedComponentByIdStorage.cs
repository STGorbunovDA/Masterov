using AutoMapper;
using Masterov.Domain.Masterov.UsedComponent.GetUsedComponentById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class GetUsedComponentByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetUsedComponentByIdStorage
{
    public async Task<UsedComponentDomain?> GetUsedComponentById(Guid usedComponentId, CancellationToken cancellationToken)
    {
        var usedComponent = await dbContext.UsedComponents
            .AsNoTracking()
            .Where(f => f.UsedComponentId == usedComponentId)
                .Include(c => c.Warehouse)
                    .ThenInclude(o => o.Supplies)
                .Include(c => c.ProductType)
            .FirstOrDefaultAsync(cancellationToken);
            
        return mapper.Map<UsedComponentDomain>(usedComponent);
    }
}