using Masterov.API.Models.ProductType;

namespace Masterov.API.Models;

public class SupplyRequest
{
    public Guid SupplyId { get; set; }
    public SupplierRequest Supplier { get; set; }
    public ProductTypeRequest ProductType { get; set; }
    public WarehouseRequest Warehouse { get; set; }
    public int Quantity { get; set; }
    public decimal PriceSupply { get; set; }
    public DateTime SupplyDate { get; set; }
}