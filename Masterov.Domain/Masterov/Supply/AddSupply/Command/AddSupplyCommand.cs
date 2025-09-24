namespace Masterov.Domain.Masterov.Supply.AddSupply.Command;

public record AddSupplyCommand(Guid SupplierId, Guid ProductTypeId, Guid WarehouseId, int Quantity, decimal PriceSupply);