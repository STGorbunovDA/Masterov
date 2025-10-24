using Masterov.API.Models.ComponentType;
using Masterov.API.Models.Supplier;
using Masterov.API.Models.Warehouse;

namespace Masterov.API.Models.Supply;

public class SupplyRequest
{
    public Guid SupplyId { get; set; }
    public SupplierResponse Supplier { get; set; }
    public ComponentTypeResponse ComponentType { get; set; }
    public WarehouseNewResponse WarehouseNew { get; set; }
    public int Quantity { get; set; }
    public decimal PriceSupply { get; set; }
    public DateTime SupplyDate { get; set; }
}