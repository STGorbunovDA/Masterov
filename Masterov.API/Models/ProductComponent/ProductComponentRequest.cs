using Masterov.API.Models.ProductType;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.ProductComponent;

public class ProductComponentRequest
{
    public Guid ProductComponentId { get; set; }
    public ProductTypeRequest ProductType { get; set; }
    public WarehouseForOrderRequest Warehouse { get; set; }
    public int Quantity { get; set; }
}