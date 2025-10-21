using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.UsedComponent.GetComponents;

public interface IGetUsedComponentsUseCase
{
    Task<IEnumerable<UsedComponentDomain>> Execute(CancellationToken cancellationToken);
}