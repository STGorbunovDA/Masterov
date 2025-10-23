namespace Masterov.Domain.Masterov.UsedComponent.UpdateUsedComponent.Command;

public record UpdateUsedComponentCommand(Guid UsedComponentId, Guid OrderId, Guid ProductTypeId, Guid WarehouseId, int Quantity, DateTime? CreatedAt);