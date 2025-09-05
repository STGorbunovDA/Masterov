using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate;

public interface IGetSuppliesBySupplyDateStorage
{
    Task<IEnumerable<SupplyDomain>?> GetSuppliesBySupplyDate(DateTime supplyDate, CancellationToken cancellationToken);
}