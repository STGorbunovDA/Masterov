using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.FinishedProduct.GetFinishedProductsByElite;

public class GetFinishedProductsByEliteUseCase(IGetFinishedProductsByEliteStorage storage) : IGetFinishedProductsByEliteUseCase
{
    public async Task<IEnumerable<FinishedProductDomain?>> Execute(CancellationToken cancellationToken) =>
        await storage.GetFinishedProductsByElite(cancellationToken);
}