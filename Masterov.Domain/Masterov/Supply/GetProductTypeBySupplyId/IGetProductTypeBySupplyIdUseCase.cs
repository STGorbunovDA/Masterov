using Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetProductTypeBySupplyId;

public interface IGetProductTypeBySupplyIdUseCase
{
    Task<ComponentTypeDomain?> Execute(GetProductTypeBySupplyIdQuery getProductTypeBySupplyId, CancellationToken cancellationToken);
}