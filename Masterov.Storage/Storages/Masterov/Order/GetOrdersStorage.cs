using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetOrdersStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetOrdersStorage
{
    public async Task<IEnumerable<OrderDomain>> GetOrders(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<OrderDomain[]>(
            nameof(GetOrders),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return dbContext.Orders
                    .AsNoTracking() 
                    .ProjectTo<OrderDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}