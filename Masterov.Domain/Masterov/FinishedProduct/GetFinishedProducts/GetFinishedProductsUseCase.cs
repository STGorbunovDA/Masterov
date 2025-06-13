using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProducts;

public class GetFinishedProductsUseCase(IGetFinishedProductsStorage storage) : IGetFinishedProductsUseCase
{
    public async Task<IEnumerable<FinishedProductDomain>> Execute(CancellationToken cancellationToken) =>
        await storage.GetFinishedProducts(cancellationToken);
}