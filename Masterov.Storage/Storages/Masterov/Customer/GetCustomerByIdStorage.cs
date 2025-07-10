using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomerById;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomerByIdStorage(MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomerByIdStorage
{
    public async Task<CustomerDomain?> GetCustomerById(Guid customerId, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<CustomerDomain?>( 
            nameof(GetCustomerById),
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
                    .Where(f => f.CustomerId == customerId)
                    .FirstOrDefaultAsync( cancellationToken);
                
                return mapper.Map<CustomerDomain>(customer);
            }))!;
}