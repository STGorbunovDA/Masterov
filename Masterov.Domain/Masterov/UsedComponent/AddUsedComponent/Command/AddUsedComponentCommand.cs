namespace Masterov.Domain.Masterov.UsedComponent.AddUsedComponent.Command;

public record AddUsedComponentCommand(Guid OrderId, Guid ProductTypeId, Guid WarehouseId, int Quantity);