﻿using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrdersByCompletedAt;

public interface IGetProductionOrdersByCompletedAtUseCase
{
    Task<IEnumerable<ProductionOrderDomain>?> Execute(GetProductionOrdersByCompletedAtQuery getProductionOrdersByCompletedAtQuery, CancellationToken cancellationToken);
}