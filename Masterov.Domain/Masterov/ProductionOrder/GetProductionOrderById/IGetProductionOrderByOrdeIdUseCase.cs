﻿using Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrderById;

public interface IGetProductionOrderByOrdeIdUseCase
{
    Task<ProductionOrderDomain?> Execute(GetProductionOrderByOrderIdQuery getProductionOrderByOrderIdQuery, CancellationToken cancellationToken);
}