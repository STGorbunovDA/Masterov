namespace Masterov.Domain.Models;

public class SupplyDomain
{
    public Guid SupplyId { get; set; }
    public SupplierDomain Supplier { get; set; }
    public ProductTypeDomain ProductType { get; set; }
    public WarehouseDomain Warehouse { get; set; }
    public int Quantity { get; set; }
    public decimal PriceSupply { get; set; }
    public DateTime SupplyDate { get; set; }
}