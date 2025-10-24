using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypes;

public interface IGetComponentTypesUseCase
{
    Task<IEnumerable<ComponentTypeDomain>> Execute(CancellationToken cancellationToken);
}