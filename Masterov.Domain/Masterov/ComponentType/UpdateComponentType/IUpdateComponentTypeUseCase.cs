using Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;
using Masterov.Domain.Models;

namespace Masterov.Domain.Masterov.ComponentType.UpdateComponentType;

public interface IUpdateComponentTypeUseCase
{
    Task<ComponentTypeDomain> Execute(UpdateComponentTypeCommand command, CancellationToken cancellationToken);
}