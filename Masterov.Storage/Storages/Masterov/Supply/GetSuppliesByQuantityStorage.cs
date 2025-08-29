using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSuppliesByQuantityStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesByQuantityStorage
{
    public async Task<IEnumerable<SupplyDomain?>> GetSuppliesByQuantity(int quantity, CancellationToken cancellationToken)
    {
       
        var supplies = await dbContext.Supplies
            .AsNoTracking() 
            .Where(p => p.Quantity == quantity)
                .Include(c => c.ProductType)
                .Include(c => c.Warehouse)
                .Include(c => c.Supplier)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}