using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

public class GetOrdersByCustomerIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetOrdersByCustomerIdStorage
{
    public async Task<IEnumerable<OrderDomain>?> GetOrdersByCustomerId(Guid customerId, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
                .Include(o => o.UsedComponents)
                    .ThenInclude(c => c.ComponentType)
                .Include(o => o.UsedComponents)
                    .ThenInclude(c => c.Warehouse)
                .Include(o => o.Payments)
            .ToListAsync(cancellationToken);

        return mapper.Map<IEnumerable<OrderDomain>>(orders);
    }
}