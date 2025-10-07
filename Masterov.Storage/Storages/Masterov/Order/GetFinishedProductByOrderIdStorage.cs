using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetFinishedProductByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetFinishedProductByOrderIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetFinishedProductByOrderIdStorage
{
    public async Task<FinishedProductDomain?> GetFinishedProductByOrderId(Guid orderId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetFinishedProductByOrderId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.Orders
                    .AsNoTracking() 
                    .Where(o => o.OrderId == orderId)
                    .Select(o => o.FinishedProduct)
                    .ProjectTo<FinishedProductDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            });
}