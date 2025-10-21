using Masterov.API.Models.ProductType;
using Masterov.API.Models.Supplier;

namespace Masterov.API.Models.Supply;

public class SupplyNoWarehouseNewResponse
{
    public Guid SupplyId { get; set; }
    public SupplierRequestNoSupply Supplier { get; set; }
    public ProductTypeResponse ProductType { get; set; }
    public int Quantity { get; set; }
    public decimal PriceSupply { get; set; }
    public DateTime SupplyDate { get; set; }
}