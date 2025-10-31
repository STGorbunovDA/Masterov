using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId;

public interface IGetComponentTypeBySupplyIdStorage
{
    Task<ComponentTypeDomain?> GetComponentTypeBySupplyId(Guid supplyId, CancellationToken cancellationToken);
}