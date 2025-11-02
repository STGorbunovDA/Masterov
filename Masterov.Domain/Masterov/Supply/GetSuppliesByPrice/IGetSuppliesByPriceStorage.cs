using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByPrice;

public interface IGetSuppliesByPriceStorage
{
    Task<IEnumerable<SupplyDomain?>> GetSuppliesByPrice(decimal price, CancellationToken cancellationToken);
}