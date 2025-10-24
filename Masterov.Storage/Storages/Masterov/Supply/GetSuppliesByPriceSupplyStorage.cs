using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSuppliesByPriceSupplyStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesByPriceSupplyStorage
{
    public async Task<IEnumerable<SupplyDomain?>> GetSuppliesByAmount(decimal priceSupply, CancellationToken cancellationToken)
    {
        var supplies = await dbContext.Supplies
            .AsNoTracking() 
            .Where(p => p.PriceSupply == priceSupply)
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse) 
                    .ThenInclude(w => w.ComponentType)
                .Include(c => c.Supplier)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}