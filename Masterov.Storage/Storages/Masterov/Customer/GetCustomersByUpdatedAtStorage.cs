using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomersByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

public class GetCustomersByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetCustomersByUpdatedAtStorage
{
    public async Task<IEnumerable<CustomerDomain>?> GetCustomersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value.Date;
        var endOfDay = startOfDay.AddDays(1);

        var customers = await dbContext.Customers
            .AsNoTracking() 
            .Where(order => order.UpdatedAt >= startOfDay && order.UpdatedAt < endOfDay)
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