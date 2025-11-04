namespace Masterov.API.Models.Warehouse;

public class UpdateWarehouseRequest
{
    public Guid ComponentTypeId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string? CreatedAt { get; set; }
}