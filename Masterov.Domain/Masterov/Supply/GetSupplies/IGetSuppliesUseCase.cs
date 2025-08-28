using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplies;

public interface IGetSuppliesUseCase
{
    Task<IEnumerable<SupplyDomain>> Execute(CancellationToken cancellationToken);
}