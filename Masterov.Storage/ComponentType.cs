using System.ComponentModel.DataAnnotations;

namespace Masterov.Storage;

public class ComponentType
{
    [Key]
    public Guid ComponentTypeId { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt  { get; set; }

    public ICollection<UsedComponent> UsedComponents { get; set; } = new List<UsedComponent>();
    public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}