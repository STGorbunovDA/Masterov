using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.UpdateComponentType;

public interface IUpdateProductTypeStorage
{
    Task<ComponentTypeDomain> UpdateComponentType(Guid componentTypeId, string name, string? description, CancellationToken cancellationToken);
}