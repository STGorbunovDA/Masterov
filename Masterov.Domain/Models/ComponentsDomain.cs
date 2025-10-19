namespace Masterov.Domain.Models;

public class ComponentsDomain
{
    public Guid ComponentId { get; set; }
    public ProductTypeDomain ProductType { get; set; }
    public WarehouseDomain Warehouse { get; set; }
    public int Quantity { get; set; }
}