namespace Masterov.Domain.Models;

public class ComponentTypeDomain
{
    public Guid ComponentTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt  { get; set; }
}