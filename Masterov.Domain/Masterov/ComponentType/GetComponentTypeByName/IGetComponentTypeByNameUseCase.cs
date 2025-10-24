using Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeByName;

public interface IGetComponentTypeByNameUseCase
{
    Task<ComponentTypeDomain?> Execute(GetComponentTypeByNameQuery getComponentTypeByNameQuery, CancellationToken cancellationToken);
}