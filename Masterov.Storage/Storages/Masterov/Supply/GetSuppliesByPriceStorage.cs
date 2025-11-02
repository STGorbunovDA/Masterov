using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSuppliesByPrice;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSuppliesByPriceStorage(MasterovDbContext dbContext, IMapper mapper) : IGetSuppliesByPriceStorage
{
    public async Task<IEnumerable<SupplyDomain?>> GetSuppliesByPrice(decimal price, CancellationToken cancellationToken)
    {
        var supplies = await dbContext.Supplies
            .AsNoTracking() 
            .Where(p => p.Price == price)
                .Include(c => c.ComponentType)
                .Include(o => o.Warehouse) 
                    .ThenInclude(w => w.ComponentType)
                .Include(c => c.Supplier)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<SupplyDomain>>(supplies);
    }
}