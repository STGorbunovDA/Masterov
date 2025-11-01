namespace Masterov.Domain.Masterov.Warehouse.UpdatePriceWarehouseById.Command;

public record UpdatePriceWarehouseByIdCommand(Guid WarehouseId, decimal Price);