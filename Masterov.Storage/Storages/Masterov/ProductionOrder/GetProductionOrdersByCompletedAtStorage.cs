﻿using AutoMapper;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;
using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCreatedAt;
using Masterov.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Masterov.Storage.Storages.Masterov.ProductionOrder;

public class GetProductionOrdersByCompletedAtStorage (MasterovDbContext dbContext, IMapper mapper) : IGetProductionOrdersByCompletedAtStorage
{
    public async Task<IEnumerable<ProductionOrderDomain>?> GetProductionOrdersByCompletedAt(DateTime completedAt, CancellationToken cancellationToken)
    {
        // Фильтрация по дате (без учёта времени)
        var startOfDay = completedAt.Date;
        var endOfDay = startOfDay.AddDays(1);

        var orders = await dbContext.ProductionOrders
            .AsNoTracking() 
            .Where(order => order.CompletedAt >= startOfDay && order.CompletedAt < endOfDay)
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