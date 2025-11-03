namespace Masterov.Domain.Masterov.Supply.UpdateSupply.Command;

public record UpdateSupplyCommand(Guid SupplyId, Guid SupplierId, Guid ComponentTypeId, Guid WarehouseId, int Quantity, decimal Price);