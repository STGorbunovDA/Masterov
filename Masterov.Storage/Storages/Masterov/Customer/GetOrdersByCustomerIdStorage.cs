using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Customer;

public class GetOrdersByCustomerIdStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetOrdersByCustomerIdStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetOrdersByCustomerId(Guid customerId, CancellationToken cancellationToken)
    {
        var cacheKey = $"GetOrdersByCustomerId_{customerId}";
        
        return (await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CacheSettings.CacheSeconds);

            var customer = await dbContext.Customers
                .AsNoTracking() 
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Components)
                        .ThenInclude(pc => pc.ProductType)
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Components)
                        .ThenInclude(pc => pc.Warehouse)
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Payments)
                        .ThenInclude(p => p.Customer)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);

            return customer == null ? null : mapper.Map<IEnumerable<ProductionOrderDomain>>(customer.Orders);
        }));
        
       
    }
}