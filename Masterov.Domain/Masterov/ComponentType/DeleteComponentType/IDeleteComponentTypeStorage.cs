namespace Masterov.Domain.Masterov.ComponentType.DeleteComponentType;

public interface IDeleteComponentTypeStorage
{
    Task<bool> DeleteComponentType(Guid componentTypeId, CancellationToken cancellationToken);
}