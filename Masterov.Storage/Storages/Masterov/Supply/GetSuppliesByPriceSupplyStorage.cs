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
                .Include(c => c.ProductType)
                .Include(c => c.Warehouse)
                .Include(c => c.Supplier)
            .Where(p => p.PriceSupply == priceSupply)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}