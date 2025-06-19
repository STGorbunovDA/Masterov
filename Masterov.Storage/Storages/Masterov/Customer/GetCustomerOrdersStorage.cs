using AutoMapper;
using Masterov.Domain.Masterov.Customer.GetCustomerOrders;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Customer;

public class GetCustomerOrdersStorage (MasterovDbContext dbContext, IMapper mapper) : IGetCustomerOrdersStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetCustomerOrders(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .Include(c => c.Orders)
            .ThenInclude(o => o.Components)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId, cancellationToken);

        return customer == null ? null : mapper.Map<IEnumerable<ProductionOrderDomain>>(customer.Orders);
    }
}