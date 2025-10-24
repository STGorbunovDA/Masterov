using AutoMapper;
using Masterov.Domain.Masterov.Supply.GetSupplyById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Supply;

internal class GetSupplyByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetSupplyByIdStorage
{
    public async Task<SupplyDomain?> GetSupplyById(Guid supplyId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<SupplyDomain?>( 
            nameof(GetSupplyById),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var supply = await dbContext.Supplies
                    .AsNoTracking()
                        .Include(c => c.ComponentType)
                        .Include(o => o.Warehouse) 
                            .ThenInclude(w => w.ComponentType)
                        .Include(c => c.Supplier)
                    .Where(f => f.SupplyId == supplyId)
                    .FirstOrDefaultAsync( cancellationToken);
                
                return mapper.Map<SupplyDomain>(supply);
            }))!;
}