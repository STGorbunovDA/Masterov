namespace Masterov.Domain.Models;

public class SupplyDomain
{
    public Guid SupplyId { get; set; }
    public SupplierDomain Supplier { get; set; }
    public ComponentTypeDomain ComponentType { get; set; }
    public WarehouseDomain Warehouse { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
}