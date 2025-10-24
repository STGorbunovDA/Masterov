namespace Masterov.Domain.Masterov.ComponentType.UpdateComponentType.Command;

public record UpdateComponentTypeCommand(Guid ComponentTypeId, string Name, string? Description);