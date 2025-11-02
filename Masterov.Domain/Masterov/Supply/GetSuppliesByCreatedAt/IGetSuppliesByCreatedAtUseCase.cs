using Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByCreatedAt;

public interface IGetSuppliesByCreatedAtUseCase
{
    Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByCreatedAtQuery getSuppliesByCreatedAtQuery, CancellationToken cancellationToken);
}