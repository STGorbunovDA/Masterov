using Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSuppliesBySupplyDate;

public interface IGetSuppliesBySupplyDateUseCase
{
    Task<IEnumerable<SupplyDomain>?> Execute(GetSuppliesBySupplyDateQuery getSuppliesBySupplyDateQuery, CancellationToken cancellationToken);
}