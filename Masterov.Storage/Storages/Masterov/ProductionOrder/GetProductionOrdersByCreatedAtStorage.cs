using AutoMapper;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

public class GetProductionOrdersByCreatedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetProductionOrdersByCreatedAtStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetProductionOrdersByCreatedAt(DateTime createdAt, CancellationToken cancellationToken)
    {
        // Фильтрация по дате (без учёта времени)
        var startOfDay = createdAt.Date;
        var endOfDay = startOfDay.AddDays(1);

        var orders = await dbContext.ProductionOrders
            .Where(order => order.CreatedAt >= startOfDay && order.CreatedAt < endOfDay)
            .Include(order => order.FinishedProduct)
            .Include(order => order.Components)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<IEnumerable<ProductionOrderDomain>>(orders);
    }
}