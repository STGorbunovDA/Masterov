using AutoMapper;
using Masterov.Domain.Masterov.Order.GetOrdersByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

public class GetOrdersByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetOrdersByCreatedAtStorage
{
    public async Task<IEnumerable<OrderDomain>?> GetOrdersByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken)
    {
        if (!createdAt.HasValue)
            return null;
        
        var startOfDay = createdAt.Value;
        var endOfDay = startOfDay.AddDays(1);

        var orders = await dbContext.Orders
            .AsNoTracking() 
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
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