using Masterov.API.Models.ComponentType;

namespace Masterov.API.Models.Warehouse;

public class WarehouseNewResponse
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public ComponentTypeResponse ComponentType { get; set; }
}