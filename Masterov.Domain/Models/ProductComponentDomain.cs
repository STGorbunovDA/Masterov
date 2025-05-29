namespace Masterov.Domain.Models;

public class ProductComponentDomain
{
    public Guid ProductComponentId { get; set; }
    public ProductTypeDomain ProductType { get; set; }
    public WarehouseDomain Warehouse { get; set; }
    public int Quantity { get; set; }
}