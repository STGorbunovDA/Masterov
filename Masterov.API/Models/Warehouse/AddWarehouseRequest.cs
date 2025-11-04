namespace Masterov.API.Models.Warehouse;

public class AddWarehouseRequest
{
    public string Name { get; set; }
    public Guid ComponentTypeId { get; set; }
}