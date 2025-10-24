namespace Masterov.API.Models.ComponentType;

public class UpdateComponentTypeRequest
{
    public Guid ComponentTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}