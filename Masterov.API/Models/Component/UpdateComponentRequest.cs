namespace Masterov.API.Models.Component;

public class UpdateComponentRequest
{
    public Guid ComponentId { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
}