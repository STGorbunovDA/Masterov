using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId;

public interface IGetSuppliesByComponentTypeIdStorage
{
    Task<IEnumerable<SupplyDomain>?> GetSuppliesByComponentTypeId(Guid componentTypeId, CancellationToken cancellationToken);
}