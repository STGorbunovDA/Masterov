using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Supply.AddSupply;
using Masterov.Domain.Models;
using Masterov.Storage.Extension;
using Microsoft.EntityFrameworkCore;


namespace Masterov.Storage.Storages.Masterov.Supply;

internal class AddSupplyStorage(MasterovDbContext dbContext, IGuidFactory guidFactory, IMapper mapper) : IAddSupplyStorage
{
    public async Task<SupplyDomain> AddSupply(Guid supplierId, Guid componentTypeId, Guid warehouseId, int quantity, decimal price, 
        CancellationToken cancellationToken)
    {
        var supplyId = guidFactory.Create();
        
        var supply = new Storage.Supply
        {
            SupplyId = supplyId,
            SupplierId = supplierId,
            ComponentTypeId = componentTypeId,
            WarehouseId = warehouseId,
            Quantity = quantity,
            Price = price,
            CreatedAt = DateTime.Now
        };

        await dbContext.Supplies.AddAsync(supply, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Supplies
            .Where(t => t.SupplyId == supplyId)
            .ProjectTo<SupplyDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}