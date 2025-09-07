using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId;

public interface IGetProductTypeBySupplyIdStorage
{
    Task<ProductTypeDomain?> GetProductTypeBySupplyId(Guid supplyId, CancellationToken cancellationToken);
}