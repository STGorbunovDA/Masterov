using AutoMapper;
using Masterov.Domain.Masterov.Order.GetCustomerByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Order;

public class GetCustomerByOrderIdStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomerByOrderIdStorage
{
    public async Task<CustomerDomain?> GetCustomerByOrderId(Guid orderId, CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync(
            nameof(GetCustomerByOrderId),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var customer = await dbContext.Orders
                    .AsNoTracking() 
                    .Where(o => o.OrderId == orderId)
                    .Select(o => o.Customer)
                    .FirstOrDefaultAsync(cancellationToken);
                
                return mapper.Map<CustomerDomain>(customer);
            });
}