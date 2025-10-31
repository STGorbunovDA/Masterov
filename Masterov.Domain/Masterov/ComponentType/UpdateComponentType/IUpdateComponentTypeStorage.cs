using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.UpdateComponentType;

public interface IUpdateComponentTypeStorage
{
    Task<ComponentTypeDomain> UpdateComponentType(Guid componentTypeId, string name, DateTime? createdAt, string? description, CancellationToken cancellationToken);
}