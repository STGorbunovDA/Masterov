using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesByQuantity;

public interface IGetSuppliesByQuantityStorage
{
    Task<IEnumerable<SupplyDomain?>> GetSuppliesByQuantity(int quantity, CancellationToken cancellationToken);
}