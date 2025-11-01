using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Order.GetOrders;

public class GetOrdersUseCase(IGetOrdersStorage storage) : IGetOrdersUseCase
{
    public async Task<IEnumerable<OrderDomain?>> Execute(CancellationToken cancellationToken) =>
        await storage.GetOrders(cancellationToken);
}