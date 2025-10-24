using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.AddComponentType;

public interface IAddComponentTypeStorage
{
    Task<ComponentTypeDomain> AddComponentType(string name, string? description, CancellationToken cancellationToken);
}