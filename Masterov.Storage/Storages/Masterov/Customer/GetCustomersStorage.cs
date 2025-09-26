using AutoMapper;
using AutoMapper.QueryableExtensions;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomersStorage(
    MasterovDbContext dbContext,
    IMemoryCache memoryCache,
    IMapper mapper) : IGetCustomersStorage
{
    public async Task<IEnumerable<CustomerDomain>> GetCustomers(CancellationToken cancellationToken)
    {
        const int cacheSeconds = 5;
        const string cacheKey = "GetCustomers_Cache";

        return (await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSeconds);

            var customers = await dbContext.Customers
                .AsNoTracking()
                .Include(c => c.Orders)
                .ThenInclude(o => o.Payments)
                .ThenInclude(p => p.Customer)
                .ToArrayAsync(cancellationToken);

            return mapper.Map<IEnumerable<CustomerDomain>>(customers);
        }))!;
    }
}