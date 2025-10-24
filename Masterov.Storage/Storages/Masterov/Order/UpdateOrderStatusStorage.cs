using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class UpdateOrderStatusStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateOrderStatusStorage
{
    public async Task<OrderDomain> UpdateOrderStatus(Guid orderId, OrderStatus orderStatus, CancellationToken cancellationToken)
    {
        var order = await dbContext.Set<Storage.Order>().FindAsync([orderId], cancellationToken);
        
        if (order == null)
            throw new Exception("order not found");
        
        order.Status = orderStatus;

        if (orderStatus == OrderStatus.Completed)
            order.CompletedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        var orderEntity = await dbContext.Orders
            .AsNoTracking() 
                .Where(f => f.OrderId == orderId)
                    .Include(o => o.FinishedProduct)
                .Include(o => o.UsedComponents)
                    .ThenInclude(c => c.ComponentType)
                .Include(o => o.UsedComponents)
                    .ThenInclude(c => c.Warehouse)
                .Include(o => o.Customer)
                .Include(o => o.Payments)
            .FirstOrDefaultAsync(cancellationToken);

        return mapper.Map<OrderDomain>(orderEntity);
    }

    
}