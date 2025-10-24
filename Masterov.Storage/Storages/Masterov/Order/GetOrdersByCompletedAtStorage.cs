using AutoMapper;
using Masterov.Domain.Masterov.Order.GetOrdersByCompletedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

public class GetOrdersByCompletedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetOrdersByCompletedAtStorage
{
    public async Task<IEnumerable<OrderDomain>?> GetOrdersByCompletedAt(DateTime? completedAt, CancellationToken cancellationToken)
    {
        if (!completedAt.HasValue)
            return null;
        
        var startOfDay = completedAt.Value;
        var endOfDay = startOfDay.AddDays(1);

        var orders = await dbContext.Orders
            .AsNoTracking() 
            .Where(order => order.CompletedAt >= startOfDay && order.CompletedAt < endOfDay)
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