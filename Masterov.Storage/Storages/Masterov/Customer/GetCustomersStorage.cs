using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomersStorage (MasterovDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : IGetCustomersStorage
{
    public async Task<IEnumerable<CustomerDomain>> GetCustomers(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(
            nameof(GetCustomers),
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var customers = await dbContext.Customers
                    .AsNoTracking()
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Payments)
                            .ThenInclude(p => p.Customer)
                    .ToArrayAsync(cancellationToken);

                return mapper.Map<CustomerDomain[]>(customers); 
            }))!;
}