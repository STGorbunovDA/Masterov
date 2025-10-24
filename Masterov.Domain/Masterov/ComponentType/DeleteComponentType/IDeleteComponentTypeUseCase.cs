using Masterov.Domain.Masterov.ComponentType.DeleteComponentType.Command;

namespace Masterov.Domain.Masterov.ComponentType.DeleteComponentType;

public interface IDeleteComponentTypeUseCase
{
    Task<bool> Execute(DeleteComponentTypeCommand deleteComponentTypeCommand, CancellationToken cancellationToken);
}