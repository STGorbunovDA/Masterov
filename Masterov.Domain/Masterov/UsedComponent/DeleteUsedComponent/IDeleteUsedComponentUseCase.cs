using Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent.Command;

namespace Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent;

public interface IDeleteUsedComponentUseCase
{
    Task<bool> Execute(DeleteUsedComponentCommand deleteUsedComponentCommand, CancellationToken cancellationToken);
}