using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

public class GetProductionOrdersByStatusStorage (MasterovDbContext dbContext, IMapper mapper) : IGetProductionOrdersByStatusStorage
{
    public async Task<IEnumerable<OrderDomain>?> GetProductionOrdersByStatus(ProductionOrderStatus status, CancellationToken cancellationToken)
    {
        var orders = await dbContext.ProductionOrders
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