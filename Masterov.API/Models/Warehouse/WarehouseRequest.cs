using Masterov.API.Models.ProductType;
namespace Masterov.API.Models.Warehouse;

public class WarehouseRequest
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; }
    public ProductTypeRequest ProductType { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}