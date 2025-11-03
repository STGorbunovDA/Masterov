using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Warehouse.GetWarehouseByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Warehouse;

internal class GetWarehouseByNameStorage(MasterovDbContext dbContext, IMapper mapper) : IGetWarehouseByNameStorage
{
    public async Task<IEnumerable<WarehouseDomain?>> GetWarehouseByName(
        string name,
        CancellationToken cancellationToken) =>
        await dbContext.Warehouses
            .AsNoTracking()
            .Where(f => f.Name.ToLower().Contains(name.ToLower().Trim()))
            .ProjectTo<WarehouseDomain>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}