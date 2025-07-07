using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.ProductionOrder.GetProductComponentAtOrder;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class GetProductComponentAtOrderStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetProductComponentAtOrderStorage
{
    public async Task<IEnumerable<ProductComponentDomain?>> GetProductComponentAtOrder(Guid orderId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetProductComponentAtOrder),
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