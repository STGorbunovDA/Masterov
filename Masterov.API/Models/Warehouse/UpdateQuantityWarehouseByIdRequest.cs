namespace Masterov.API.Models.Warehouse;

public class UpdateQuantityWarehouseByIdRequest
{
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
}