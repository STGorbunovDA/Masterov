using AutoMapper;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByDescription;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

public class GetProductionOrdersByDescriptionStorage (MasterovDbContext dbContext, IMapper mapper) : IGetProductionOrdersByDescriptionStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetProductionOrdersByDescription(string description, CancellationToken cancellationToken)
    {
        var orders = await dbContext.ProductionOrders
            .Where(order => order.Description != null && order.Description.Contains(description))
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ProductionOrderDomain>>(orders);
    }
}