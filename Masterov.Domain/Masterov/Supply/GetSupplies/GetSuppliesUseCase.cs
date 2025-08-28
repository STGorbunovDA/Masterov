using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplies;

public class GetSuppliesUseCase(IGetSuppliesStorage storage) : IGetSuppliesUseCase
{
    public async Task<IEnumerable<SupplyDomain>> Execute(CancellationToken cancellationToken)
    {
        return await storage.GetSupplies(cancellationToken);
    }
}