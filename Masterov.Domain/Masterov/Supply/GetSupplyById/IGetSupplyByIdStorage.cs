using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetSupplyById;

public interface IGetSupplyByIdStorage
{
    Task<SupplyDomain?> GetSupplyById(Guid supplyId, CancellationToken cancellationToken);
}