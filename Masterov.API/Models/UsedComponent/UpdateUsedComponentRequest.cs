namespace Masterov.API.Models.UsedComponent;

public class UpdateUsedComponentRequest
{
    public Guid UsedComponentId { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
}