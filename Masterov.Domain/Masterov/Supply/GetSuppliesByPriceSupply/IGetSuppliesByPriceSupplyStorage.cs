using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPriceSupply;

public interface IGetSuppliesByPriceSupplyStorage
{
    Task<IEnumerable<SupplyDomain?>> GetSuppliesByAmount(decimal priceSupply, CancellationToken cancellationToken);
}