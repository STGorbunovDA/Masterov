using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Warehouse.GetSuppliesByWarehouseId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetSuppliesByWarehouseIdStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesByWarehouseIdStorage
{
    public async Task<IEnumerable<SupplyDomain>?> GetSuppliesByWarehouseId(Guid warehouseId, CancellationToken cancellationToken) =>
        await dbContext.Warehouses
            .AsNoTracking()
            .Where(o => o.WarehouseId == warehouseId)
            .SelectMany(o => o.Supplies)
            .ProjectTo<SupplyDomain>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
}