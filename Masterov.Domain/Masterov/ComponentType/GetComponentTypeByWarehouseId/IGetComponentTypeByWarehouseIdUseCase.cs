using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByWarehouseId;

public interface IGetComponentTypeByWarehouseIdUseCase
{
    Task<ComponentTypeDomain?> Execute(GetComponentTypeByWarehouseIdQuery query, CancellationToken cancellationToken);
}