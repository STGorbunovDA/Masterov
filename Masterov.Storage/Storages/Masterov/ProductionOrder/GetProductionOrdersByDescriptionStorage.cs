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
            .AsNoTracking() 
            .Where(order => order.Description != null && order.Description.Contains(description))
            .Include(order => order.FinishedProduct)
            .Include(order => order.Components)
            .ThenInclude(c => c.ProductType)
            .Include(order => order.Components)
            .ThenInclude(c => c.Warehouse)
            .Include(o => o.Customer)
            .Include(o => o.Payments)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ProductionOrderDomain>>(orders);
    }
}