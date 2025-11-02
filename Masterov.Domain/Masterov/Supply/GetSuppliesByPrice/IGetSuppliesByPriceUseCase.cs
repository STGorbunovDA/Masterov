using Masterov.Domain.Masterov.Supply.GetSuppliesByPrice.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPrice;

public interface IGetSuppliesByPriceUseCase
{
    Task<IEnumerable<SupplyDomain?>> Execute(GetSuppliesByPriceQuery getSuppliesByPriceQuery, CancellationToken cancellationToken);
}