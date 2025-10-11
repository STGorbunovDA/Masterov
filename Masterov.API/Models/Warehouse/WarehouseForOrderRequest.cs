namespace Masterov.API.Models.Warehouse;

public class WarehouseForOrderRequest
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; }
}