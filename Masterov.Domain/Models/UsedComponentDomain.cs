namespace Masterov.Domain.Models;

public class UsedComponentDomain
{
    public Guid UsedComponentId { get; set; }
    public ProductTypeDomain ProductType { get; set; }
    public WarehouseDomain Warehouse { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}