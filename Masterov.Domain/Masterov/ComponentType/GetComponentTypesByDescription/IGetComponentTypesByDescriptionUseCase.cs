using Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription.Query;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypesByDescription;

public interface IGetComponentTypesByDescriptionUseCase
{
    Task<IEnumerable<ComponentTypeDomain>?> Execute(GetComponentTypesByDescriptionQuery componentTypesByDescriptionQuery, CancellationToken cancellationToken);
}