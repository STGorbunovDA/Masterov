using AutoMapper;
using Masterov.Domain.Masterov.Order.GetOrdersByUpdatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

public class GetOrdersByUpdatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetOrdersByUpdatedAtStorage
{
    public async Task<IEnumerable<OrderDomain>?> GetOrdersByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken)
    {
        if (!updatedAt.HasValue)
            return null;
        
        var startOfDay = updatedAt.Value;
        var endOfDay = startOfDay.AddDays(1);

        var orders = await dbContext.Orders
            .AsNoTracking() 
            .Where(order => order.UpdatedAt >= startOfDay && order.UpdatedAt < endOfDay)
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