namespace Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent;

public interface IDeleteUsedComponentStorage
{
    Task<bool> DeleteUsedComponent(Guid usedComponentId, CancellationToken cancellationToken);
}