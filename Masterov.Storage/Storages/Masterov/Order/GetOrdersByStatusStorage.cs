using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Order.GetOrdersByStatus;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

public class GetOrdersByStatusStorage (MasterovDbContext dbContext, IMapper mapper) : IGetOrdersByStatusStorage
{
    public async Task<IEnumerable<OrderDomain>?> GetOrdersByStatus(OrderStatus status, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking() 
            .Where(order => order.Status == status)
                .Include(order => order.FinishedProduct)
                .Include(order => order.Components)
                    .ThenInclude(c => c.ProductType)
                .Include(order => order.Components)
                    .ThenInclude(c => c.Warehouse)
                .Include(o => o.Customer)
                .Include(o => o.Payments)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<OrderDomain>>(orders);
    }
}