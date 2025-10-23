using Masterov.API.Models.ProductType;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.UsedComponent;

public class UsedComponentResponse
{
    public Guid UsedComponentId { get; set; }
    public ProductTypeResponse ProductType { get; set; }
    public WarehouseForOrderRequest Warehouse { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}