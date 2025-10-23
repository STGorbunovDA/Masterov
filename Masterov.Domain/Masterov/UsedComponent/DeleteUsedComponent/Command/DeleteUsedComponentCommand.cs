namespace Masterov.Domain.Masterov.UsedComponent.DeleteUsedComponent.Command;

public record DeleteUsedComponentCommand(Guid UsedComponentId, bool DeleteWarehouse);