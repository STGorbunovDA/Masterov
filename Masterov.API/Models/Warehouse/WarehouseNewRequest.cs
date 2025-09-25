using Masterov.API.Models.ProductType;

namespace Masterov.API.Models.Warehouse;

public class WarehouseNewRequest
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal LastPurchasePrice { get; set; }
    public ProductTypeRequest ProductType { get; set; }
}