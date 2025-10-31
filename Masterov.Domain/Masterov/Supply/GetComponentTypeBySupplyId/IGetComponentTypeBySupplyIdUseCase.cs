using Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Supply.GetComponentTypeBySupplyId;

public interface IGetComponentTypeBySupplyIdUseCase
{
    Task<ComponentTypeDomain?> Execute(GetComponentTypeBySupplyIdQuery getComponentTypeBySupplyId, CancellationToken cancellationToken);
}