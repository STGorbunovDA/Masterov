using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetWarehouseByIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetWarehouseByIdStorage
{
    public async Task<WarehouseDomain?> GetWarehouseById(Guid warehouseId, CancellationToken cancellationToken) =>
        await dbContext.Warehouses
            .AsNoTracking()
            .Where(f => f.WarehouseId == warehouseId)
            .ProjectTo<WarehouseDomain>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}