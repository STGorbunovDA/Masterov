using AutoMapper;
using Masterov.Domain.Masterov.Warehouse.GetWarehouses;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetWarehousesStorage(MasterovDbContext dbContext, IMapper mapper) : IGetWarehousesStorage
{
    public async Task<IEnumerable<WarehouseDomain>> GetWarehouses(CancellationToken cancellationToken)
    {
        var warehouses = await dbContext.Warehouses
            .AsNoTracking()
                .Include(c => c.ComponentType)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<WarehouseDomain[]>(warehouses);
    }
}