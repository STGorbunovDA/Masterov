using Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply;

public interface IGetSuppliesByPriceSupplyUseCase
{
    Task<IEnumerable<SupplyDomain?>> Execute(GetSuppliesByAmountPriceSupply getSuppliesByAmountPriceSupply, CancellationToken cancellationToken);
}