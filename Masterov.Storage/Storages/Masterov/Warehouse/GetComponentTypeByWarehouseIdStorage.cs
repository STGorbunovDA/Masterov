using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetComponentTypeByWarehouseIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetComponentTypeByWarehouseIdStorage
{
    public async Task<ComponentTypeDomain?> GetComponentTypeByWarehouseId(Guid warehouseId, CancellationToken cancellationToken) =>
        await dbContext.Warehouses
            .AsNoTracking()
            .Where(o => o.WarehouseId == warehouseId)
            .Select(o => o.ComponentType)
            .ProjectTo<ComponentTypeDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}