namespace Masterov.API.Models.Supply;

public class AddSupplyRequest
{
    public Guid SupplierId { get; set; }
    public Guid ComponentTypeId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceSupply { get; set; }
}