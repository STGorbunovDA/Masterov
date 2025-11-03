namespace Masterov.Domain.Models;

public class WarehouseDomain
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; }
    public ComponentTypeDomain ComponentType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public IEnumerable<SupplyDomain> Supplies { get; set; }
}