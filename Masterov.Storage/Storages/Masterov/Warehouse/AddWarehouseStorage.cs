using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Warehouse.AddWarehouse;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class AddWarehouseStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddWarehouseStorage
{
    public async Task<WarehouseDomain> AddWarehouse(string name, Guid componentTypeId, 
        CancellationToken cancellationToken)
    {
        var warehouseId = guidFactory.Create();
        
        var warehouse = new Storage.Warehouse()
        {
            WarehouseId = warehouseId,
            Name = name,
            ComponentTypeId = componentTypeId,
            CreatedAt = DateTime.Now
        };

        await dbContext.Warehouses.AddAsync(warehouse, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Warehouses
            .Where(t => t.WarehouseId == warehouseId)
            .ProjectTo<WarehouseDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}