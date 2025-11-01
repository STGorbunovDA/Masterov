using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomers;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

internal class GetCustomersStorage(MasterovDbContext dbContext, IMapper mapper) : IGetCustomersStorage
{
    public async Task<IEnumerable<CustomerDomain?>> GetCustomers(CancellationToken cancellationToken)
    {
        var customers = await dbContext.Customers
            .AsNoTracking()
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Payments)
                    .ThenInclude(p => p.Customer)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<CustomerDomain>>(customers);
    }
}