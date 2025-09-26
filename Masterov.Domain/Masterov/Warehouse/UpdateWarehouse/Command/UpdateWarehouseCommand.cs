namespace Masterov.Domain.Masterov.Warehouse.UpdateWarehouse.Command;

public record UpdateWarehouseCommand(Guid WarehouseId, Guid ProductTypeId, string Name, int Quantity, decimal Price);