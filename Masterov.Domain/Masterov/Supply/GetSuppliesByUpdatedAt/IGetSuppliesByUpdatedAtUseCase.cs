using Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByUpdatedAt;

public interface IGetSuppliesByUpdatedAtUseCase
{
    Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByUpdatedAtQuery getSuppliesByUpdatedAtQuery, CancellationToken cancellationToken);
}