using Masterov.API.Models.ProductType;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.UsedComponent;

public class UsedComponentNewRequest
{
    public Guid UsedComponentId { get; set; }
    public ProductTypeResponse ProductType { get; set; }
    public WarehouseNewNoProductTypeRequest Warehouse { get; set; }
    public int Quantity { get; set; }
}