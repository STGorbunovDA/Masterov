using Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId;

public interface IGetProductTypeBySupplyIdUseCase
{
    Task<ProductTypeDomain?> Execute(GetProductTypeBySupplyIdQuery getProductTypeBySupplyId, CancellationToken cancellationToken);
}