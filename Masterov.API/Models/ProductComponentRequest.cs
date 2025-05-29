using Masterov.API.Models.ProductType;

namespace Masterov.API.Models;

public class ProductComponentRequest
{
    public Guid ProductComponentId { get; set; }
    public ProductTypeRequest ProductType { get; set; }
    public WarehouseRequest Warehouse { get; set; }
    public int Quantity { get; set; }
}