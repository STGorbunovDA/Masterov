using Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetComponentTypeByUsedComponentId;

public interface IGetComponentTypeByUsedComponentIdUseCase
{
    Task<ComponentTypeDomain?> Execute(GetComponentTypeByUsedComponentIdQuery componentTypeByUsedComponentIdQuery, CancellationToken cancellationToken);
}