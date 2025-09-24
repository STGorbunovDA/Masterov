namespace Masterov.Domain.Masterov.Supply.UpdateSupply.Command;

public record UpdateSupplyCommand(Guid SupplyId, Guid SupplierId, Guid ProductTypeId, Guid WarehouseId, int Quantity, decimal PriceSupply);