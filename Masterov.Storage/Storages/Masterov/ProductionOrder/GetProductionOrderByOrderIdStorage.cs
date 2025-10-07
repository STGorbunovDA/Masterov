using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class GetProductionOrderByOrderIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductionOrderByOrderIdStorage
{
    public async Task<OrderDomain?> GetProductionOrderById(Guid orderId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<OrderDomain?>( 
            nameof(GetProductionOrderById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ProductionOrders
                    .AsNoTracking() 
                    .Where(f => f.OrderId == orderId)
                    .ProjectTo<OrderDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}