using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductionOrder.GetFinishedProductAtOrder;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class GetFinishedProductAtOrderStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetFinishedProductAtOrderStorage
{
    public async Task<FinishedProductDomain?> GetFinishedProductAtOrder(Guid orderId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetFinishedProductAtOrder),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.ProductionOrders
                    .Where(o => o.OrderId == orderId)
                    .Select(o => o.FinishedProduct)
                    .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            });
}