using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.ProductionOrder.UpdateProductionOrder;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

internal class UpdateProductionOrderStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateProductionOrderStorage
{
    public async Task<ProductionOrderDomain> UpdateProductionOrder(Guid orderId, DateTime createdAt, ProductionOrderStatus status, string? description,
        Guid customerId, CancellationToken ct)
    {
        var order = await dbContext.Set<Storage.ProductionOrder>().FindAsync([orderId], ct);
        
        if (order == null)
            throw new Exception("ProductionOrder not found");

        order.CustomerId = customerId;
        order.Status = status;
        order.CreatedAt = createdAt;
        order.CompletedAt = DateTime.Now;
        if(description is not null)
            order.Description = description;
        
        await dbContext.SaveChangesAsync(ct);
        
        var orderDb = await dbContext.ProductionOrders
            .AsNoTracking()
            .Where(t => t.OrderId == order.OrderId)
            .Include(c => c.FinishedProduct)
            .Include(c => c.Customer)
            .FirstOrDefaultAsync(ct);
        
        return mapper.Map<ProductionOrderDomain>(orderDb);
    }
}