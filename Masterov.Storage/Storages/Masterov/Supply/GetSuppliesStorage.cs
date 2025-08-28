using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSupplies;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSuppliesStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSuppliesStorage
{
    public async Task<IEnumerable<SupplyDomain>> GetSupplies(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(
            nameof(GetSupplies),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var supplies = await dbContext.Supplies
                    .AsNoTracking()
                        .Include(c => c.Supplier)
                        .Include(c => c.ProductType)
                        .Include(c => c.Warehouse)
                    .ToArrayAsync(cancellationToken);

                return mapper.Map<SupplyDomain[]>(supplies); 
            }))!;
}