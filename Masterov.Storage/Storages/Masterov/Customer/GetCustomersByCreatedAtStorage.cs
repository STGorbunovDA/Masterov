using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomersByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

public class GetCustomersByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetCustomersByCreatedAtStorage
{
    public async Task<IEnumerable<CustomerDomain>?> GetCustomersByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var customers = await dbContext.Customers
            .AsNoTracking() 
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
                .Include(order => order.Orders)
                    .ThenInclude(c => c.FinishedProduct)
                .Include(order => order.Orders)
                    .ThenInclude(c => c.Components)
                .Include(order => order.Orders)
                    .ThenInclude(c => c.Payments)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<CustomerDomain>>(customers);
    }
}