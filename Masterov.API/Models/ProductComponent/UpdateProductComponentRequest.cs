namespace Masterov.API.Models.ProductComponent;

public class UpdateProductComponentRequest
{
    public Guid ProductComponentId { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
}