namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;

public record UpdateWarehouseCommand(Guid WarehouseId, Guid ComponentTypeId, string Name, int Quantity, decimal Price, DateTime? CreatedAt);