using AutoMapper;
using Masterov.Domain.Masterov.ProductionOrder.GetCustomerByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

public class GetCustomerByOrderIdStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomerByOrderIdStorage
{
    public async Task<CustomerDomain?> GetCustomerByOrderId(Guid orderId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetCustomerByOrderId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var customer = await dbContext.ProductionOrders
                    .AsNoTracking() 
                    .Where(o => o.OrderId == orderId)
                    .Select(o => o.Customer)
                    .FirstOrDefaultAsync(cancellationToken);
                
                return mapper.Map<CustomerDomain>(customer);
            });
}