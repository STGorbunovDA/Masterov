using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetUsedComponents;

public interface IGetUsedComponentsUseCase
{
    Task<IEnumerable<UsedComponentDomain?>> Execute(CancellationToken cancellationToken);
}