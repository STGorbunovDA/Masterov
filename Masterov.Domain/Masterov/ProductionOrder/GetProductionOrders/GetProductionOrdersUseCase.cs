using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ProductionOrder.GetProductionOrders;

public class GetProductionOrdersUseCase(IGetProductionOrdersStorage storage) : IGetProductionOrdersUseCase
{
    public async Task<IEnumerable<OrderDomain>> Execute(CancellationToken cancellationToken) =>
        await storage.GetProductionOrders(cancellationToken);
}