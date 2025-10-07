using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Order.GetProductComponentByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class GetProductComponentByOrderIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductComponentByOrderIdStorage
{
    public async Task<IEnumerable<ProductComponentDomain?>> GetProductComponentByOrderId(Guid orderId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetProductComponentByOrderId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                return await dbContext.ProductComponents
                    .AsNoTracking() 
                    .Where(pc => pc.OrderId == orderId)
                    .ProjectTo<ProductComponentDomain>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }) ?? throw new InvalidOperationException();
}