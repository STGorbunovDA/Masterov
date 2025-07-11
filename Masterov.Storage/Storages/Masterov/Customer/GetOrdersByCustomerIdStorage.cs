using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetOrdersByCustomerId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

public class GetOrdersByCustomerIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetOrdersByCustomerIdStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetCustomerOrders(
        Guid customerId,
        CancellationToken cancellationToken)
    {
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
    }
}