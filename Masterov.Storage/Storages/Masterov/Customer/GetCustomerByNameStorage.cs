using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Customer.GetCustomerByName;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByNameStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomerByNameStorage
{
    public async Task<CustomerDomain?> GetCustomerByName(string customerName, CancellationToken cancellationToken)=>
        (await memoryCache.GetOrCreateAsync<CustomerDomain?>( 
            nameof(GetCustomerByName),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                
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
                    .Where(f => f.Email != null && f.Name.ToLower() == customerName.ToLower().Trim())
                    .FirstOrDefaultAsync(cancellationToken);
                
                return mapper.Map<CustomerDomain>(customer);
            }))!;
}