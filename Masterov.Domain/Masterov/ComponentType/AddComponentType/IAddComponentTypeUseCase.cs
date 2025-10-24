using Masterov.Domain.Masterov.ComponentType.AddComponentType.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.AddComponentType;

public interface IAddComponentTypeUseCase
{
    Task<ComponentTypeDomain> Execute(AddComponentTypeCommand addComponentTypeCommand, CancellationToken cancellationToken);
}