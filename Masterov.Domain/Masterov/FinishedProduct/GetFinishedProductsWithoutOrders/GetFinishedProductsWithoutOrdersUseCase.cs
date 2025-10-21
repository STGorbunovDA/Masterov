using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsWithoutOrders;

public class GetFinishedProductsWithoutOrdersUseCase(IGetFinishedProductsWithoutOrdersStorage storage) : IGetFinishedProductsWithoutOrdersUseCase
{
    public async Task<IEnumerable<FinishedProductNoOrdersDomain>> Execute(CancellationToken cancellationToken) =>
        await storage.GetFinishedProductsWithoutOrders(cancellationToken);
}