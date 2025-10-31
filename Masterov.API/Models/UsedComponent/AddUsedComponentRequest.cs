namespace Masterov.API.Models.UsedComponent;

public class AddUsedComponentRequest
{
    public Guid OrderId { get; set; }
    public Guid ComponentTypeId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
}