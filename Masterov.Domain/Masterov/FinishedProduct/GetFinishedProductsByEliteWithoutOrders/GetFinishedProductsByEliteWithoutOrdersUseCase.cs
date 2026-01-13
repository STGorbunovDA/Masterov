using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByEliteWithoutOrders;

public class GetFinishedProductsByEliteWithoutOrdersUseCase(IGetFinishedProductsByEliteWithoutOrdersStorage storage) : IGetFinishedProductsByEliteWithoutOrdersUseCase
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain?>> Execute(CancellationToken cancellationToken) =>
        await storage.GetFinishedProductsByEliteWithoutOrders(cancellationToken);
}