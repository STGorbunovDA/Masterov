using Masterov.API.Models.ProductType;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.Supply;

public class SupplyRequest
{
    public Guid SupplyId { get; set; }
    public SupplierResponse Supplier { get; set; }
    public ProductTypeResponse ProductType { get; set; }
    public WarehouseNewResponse WarehouseNew { get; set; }
    public int Quantity { get; set; }
    public decimal PriceSupply { get; set; }
    public DateTime SupplyDate { get; set; }
}