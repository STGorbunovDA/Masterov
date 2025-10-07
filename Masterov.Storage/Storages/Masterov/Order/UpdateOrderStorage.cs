using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Order.UpdateOrder;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class UpdateOrderStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateOrderStorage
{
    public async Task<OrderDomain> UpdateOrder(Guid orderId, DateTime createdAt, OrderStatus status, string? description,
        Guid customerId, CancellationToken ct)
    {
        var order = await dbContext.Set<Storage.Order>().FindAsync([orderId], ct);
        
        if (order == null)
            throw new Exception("ProductionOrder not found");

        order.CustomerId = customerId;
        order.Status = status;
        order.CreatedAt = createdAt;
        order.CompletedAt = DateTime.Now;
        if(description is not null)
            order.Description = description;
        
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