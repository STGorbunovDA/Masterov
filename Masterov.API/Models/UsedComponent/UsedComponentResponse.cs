using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.UsedComponent;

public class UsedComponentResponse
{
    public Guid UsedComponentId { get; set; }
    public ComponentTypeResponse ComponentType { get; set; }
    public WarehouseForOrderRequest Warehouse { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}