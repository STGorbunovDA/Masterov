namespace Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent.Command;

public record UpdateUsedComponentCommand(Guid UsedComponentId, Guid OrderId, Guid ComponentTypeId, Guid WarehouseId, int Quantity, DateTime? CreatedAt);