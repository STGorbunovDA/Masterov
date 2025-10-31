namespace Masterov.Domain.Masterov.UsedComponent.AddUsedComponent.Command;

public record AddUsedComponentCommand(Guid OrderId, Guid ComponentTypeId, Guid WarehouseId, int Quantity);