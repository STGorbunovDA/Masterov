using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByName;

public interface IGetComponentTypesByNameUseCase
{
    Task<IEnumerable<ComponentTypeDomain?>> Execute(GetComponentTypesByNameQuery getComponentTypesByNameQuery, CancellationToken cancellationToken);
}