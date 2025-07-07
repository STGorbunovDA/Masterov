using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class GetProductionOrdersStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductionOrdersStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>> GetProductionOrders(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ProductionOrderDomain[]>(
            nameof(GetProductionOrders),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ProductionOrders
                    .AsNoTracking() 
                    .ProjectTo<ProductionOrderDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}