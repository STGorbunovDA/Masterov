namespace Masterov.Domain.Masterov.Warehouse.AddWarehouse.Command;

public record AddWarehouseCommand(string Name, Guid ComponentTypeId);