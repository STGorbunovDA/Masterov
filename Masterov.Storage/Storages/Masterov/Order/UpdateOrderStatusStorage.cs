using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.Order.UpdateOrderStatus;
using Masterov.Domain.Models;

namespace Masterov.Storage.Storages.Masterov.Order;

internal class UpdateOrderStatusStorage(MasterovDbContext dbContext, IMapper mapper) : IUpdateOrderStatusStorage
{
    public async Task<OrderDomain> UpdateOrderStatus(Guid orderId, OrderStatus status, CancellationToken cancellationToken)
    {
        var productionOrder = await dbContext.Set<Storage.Order>().FindAsync([orderId], cancellationToken);
        
        if (productionOrder == null)
            throw new Exception("ProductionOrder not found");
        
        productionOrder.Status = status;

        if (status == OrderStatus.Completed)
            productionOrder.CompletedAt = DateTime.Now;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<OrderDomain>(productionOrder);
    }

    
}