using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.Component.GetComponents;

public interface IGetComponentsUseCase
{
    Task<IEnumerable<ComponentsDomain>> Execute(CancellationToken cancellationToken);
}