namespace Masterov.API.Models.Warehouse;

public class UpdateWarehouseRequest
{
    public Guid WarehouseId { get; set; }
    public Guid ProductTypeId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}