using Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity;

public interface IGetSuppliesByQuantityUseCase
{
    Task<IEnumerable<SupplyDomain?>> Execute(GetSuppliesByQuantityQuery getSuppliesByQuantityQuery, CancellationToken cancellationToken);
}