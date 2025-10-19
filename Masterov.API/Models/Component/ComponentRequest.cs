using Masterov.API.Models.ProductType;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.Component;

public class ComponentRequest
{
    public Guid ComponentId { get; set; }
    public ProductTypeRequest ProductType { get; set; }
    public WarehouseForOrderRequest Warehouse { get; set; }
    public int Quantity { get; set; }
}