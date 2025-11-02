using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplies;

public interface IGetSuppliesStorage
{
    Task<IEnumerable<SupplyDomain?>> GetSupplies(CancellationToken cancellationToken);
}