using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetOrderById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetOrderByOrderIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetOrderByOrderIdStorage
{
    public async Task<OrderDomain?> GetOrderByOrderId(Guid orderId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<OrderDomain?>( 
            nameof(GetOrderByOrderId),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Orders
                    .AsNoTracking() 
                    .Where(f => f.OrderId == orderId)
                    .ProjectTo<OrderDomain>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);
            }))!;
}