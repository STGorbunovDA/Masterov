using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Customer.GetCustomerByPhone;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByPhoneStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomerByPhoneStorage
{
    public async Task<CustomerDomain?> GetCustomerByPhone(string customerPhone, CancellationToken cancellationToken)
    {
        var cacheKey = $"GetCustomerByPhone_{customerPhone}";
        
        return (await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CacheSettings.CacheSeconds);

            var customer = await dbContext.Customers
                .AsNoTracking() 
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Payments)
                        .ThenInclude(p => p.Customer)
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Components)
                        .ThenInclude(pc => pc.ProductType)
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Components)
                        .ThenInclude(pc => pc.Warehouse)
                .Where(f => f.Phone != null && f.Phone.ToLower() == customerPhone.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);
                
            return mapper.Map<CustomerDomain>(customer);
                
            return mapper.Map<CustomerDomain>(customer);
        }));
    }
}