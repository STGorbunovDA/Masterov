using Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetSuppliesByComponentTypeId;

public interface IGetSuppliesByComponentTypeIdUseCase
{
    Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesByComponentTypeIdQuery suppliesByComponentTypeIdQuery, CancellationToken cancellationToken);
}