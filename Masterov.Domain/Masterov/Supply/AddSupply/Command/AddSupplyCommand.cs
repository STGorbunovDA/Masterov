namespace Masterov.Domain.Masterov.Supply.AddSupply.Command;

public record AddSupplyCommand(Guid SupplierId, Guid ComponentTypeId, Guid WarehouseId, int Quantity, decimal Price);