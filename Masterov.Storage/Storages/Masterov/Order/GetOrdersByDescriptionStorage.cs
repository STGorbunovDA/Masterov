using AutoMapper;
using Masterov.Domain.Masterov.Order.GetOrdersByDescription;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

public class GetOrdersByDescriptionStorage (MasterovDbContext dbContext, IMapper mapper) : IGetOrdersByDescriptionStorage
{
    public async Task<IEnumerable<OrderDomain>?> GetOrdersByDescription(string description, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking() 
            .Where(order => order.Description != null && order.Description.Contains(description))
                .Include(order => order.FinishedProduct)
                .Include(order => order.UsedComponents)
                    .ThenInclude(c => c.ComponentType)
                .Include(order => order.UsedComponents)
                    .ThenInclude(c => c.Warehouse)
                .Include(o => o.Customer)
                .Include(o => o.Payments)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<OrderDomain>>(orders);
    }
}