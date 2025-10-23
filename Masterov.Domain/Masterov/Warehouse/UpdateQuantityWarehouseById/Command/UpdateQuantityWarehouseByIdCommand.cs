namespace Masterov.Domain.Masterov.Warehouse.UpdateQuantityWarehouseById.Command;

public record UpdateQuantityWarehouseByIdCommand(Guid WarehouseId, int Quantity);