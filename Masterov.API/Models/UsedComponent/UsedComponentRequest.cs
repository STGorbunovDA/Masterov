using Masterov.API.Models.ProductType;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.UsedComponent;

public class UsedComponentRequest
{
    public Guid UsedComponentId { get; set; }
    public ProductTypeRequest ProductType { get; set; }
    public WarehouseForOrderRequest Warehouse { get; set; }
    public int Quantity { get; set; }
}