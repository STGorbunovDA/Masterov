using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.UsedComponent.GetWarehouseByUsedComponentId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.UsedComponent;

internal class GetWarehouseByUsedComponentIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetWarehouseByUsedComponentIdStorage
{
    public async Task<WarehouseDomain?> GetWarehouseByUsedComponentId(Guid usedComponentId, CancellationToken cancellationToken) =>
        await dbContext.UsedComponents
            .AsNoTracking() 
            .Where(o => o.UsedComponentId == usedComponentId)
            .Select(o => o.Warehouse)
            .ProjectTo<WarehouseDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}