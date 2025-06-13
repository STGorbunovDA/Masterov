using AutoMapper;
using Masterov.Domain.Extension;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByStatus;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

public class GetProductionOrdersByStatusStorage (MasterovDbContext dbContext, IMapper mapper) : IGetProductionOrdersByStatusStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetProductionOrdersByStatus(ProductionOrderStatus status, CancellationToken cancellationToken)
    {
        var orders = await dbContext.ProductionOrders
            .Where(order => order.Status == status)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ProductionOrderDomain>>(orders);
    }
}