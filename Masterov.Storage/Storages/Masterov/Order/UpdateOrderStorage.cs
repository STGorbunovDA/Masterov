using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Order.UpdateOrder;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class UpdateOrderStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateOrderStorage
{
    public async Task<OrderDomain> UpdateOrder(Guid orderId, DateTime? createdAt, DateTime? completedAt, OrderStatus status, string? description,
        Guid customerId, Guid finishedProductId, CancellationToken ct)
    {
        var order = await dbContext.Set<Storage.Order>().FindAsync([orderId], ct);
        
        if (order == null)
            throw new Exception("Order not found");

        if (createdAt.HasValue)
            order.CreatedAt = createdAt.Value;
        if (completedAt.HasValue)
            order.CompletedAt = completedAt.Value;
        
        order.Status = status;
        
        if(description is not null)
            order.Description = description;
        
        order.CustomerId = customerId;
        order.FinishedProductId = finishedProductId;
        
        order.UpdatedAt = DateTime.Now;
        
        
        await dbContext.SaveChangesAsync(ct);
        
        var orderDb = await dbContext.Orders
            .AsNoTracking()
            .Where(t => t.OrderId == order.OrderId)
            .Include(c => c.FinishedProduct)
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(ct);
        
        return mapper.Map<OrderDomain>(orderDb);
    }
}