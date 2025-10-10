using AutoMapper;
using Masterov.Domain.Masterov.Order.GetCustomerByOrderId;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

public class GetCustomerByOrderIdStorage (MasterovDbContext dbContext, IMapper mapper) : IGetCustomerByOrderIdStorage
{
    public async Task<CustomerDomain?> GetCustomerByOrderId(Guid orderId, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Orders
            .AsNoTracking() 
            .Where(o => o.OrderId == orderId)
            .Select(o => o.Customer)
            .FirstOrDefaultAsync(cancellationToken);
        
        return mapper.Map<CustomerDomain>(customer);
    }
}