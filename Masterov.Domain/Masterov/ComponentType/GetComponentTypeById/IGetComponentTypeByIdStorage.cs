using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.GetComponentTypeById;

public interface IGetComponentTypeByIdStorage
{
    Task<ComponentTypeDomain?> GetComponentTypeById(Guid componentTypeId, CancellationToken cancellationToken);
}