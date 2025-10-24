using Masterov.Domain.Masterov.ComponentType.GetComponentTypeById.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;

public interface IGetComponentTypeByIdUseCase
{
    Task<ComponentTypeDomain?> Execute(GetComponentTypeByIdQuery getComponentTypeByIdQuery, CancellationToken cancellationToken);
}