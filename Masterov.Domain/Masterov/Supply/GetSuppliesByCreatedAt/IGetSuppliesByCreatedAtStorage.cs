using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt;

public interface IGetSuppliesByCreatedAtStorage
{
    Task<IEnumerable<SupplyDomain>?> GetSuppliesByCreatedAt(DateTime? createdAt, CancellationToken cancellationToken);
}