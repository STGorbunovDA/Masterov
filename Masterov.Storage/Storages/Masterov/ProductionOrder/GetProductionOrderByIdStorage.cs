using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class GetProductionOrderByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductionOrderByIdStorage
{
    public async Task<ProductionOrderDomain?> GetProductionOrderById(Guid orderId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ProductionOrderDomain?>( 
            nameof(GetProductionOrderById),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.ProductionOrders
                    .Where(f => f.FinishedProductId == orderId)
                    .ProjectTo<ProductionOrderDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}