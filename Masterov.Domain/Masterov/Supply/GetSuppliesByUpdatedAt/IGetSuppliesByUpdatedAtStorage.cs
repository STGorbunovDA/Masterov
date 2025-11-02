using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt;

public interface IGetSuppliesByUpdatedAtStorage
{
    Task<IEnumerable<SupplyDomain>?> GetSuppliesByUpdatedAt(DateTime? updatedAt, CancellationToken cancellationToken);
}