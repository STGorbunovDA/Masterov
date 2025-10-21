using System.ComponentModel.DataAnnotations;

namespace Masterov.Storage;

public class ProductType
{
    [Key]
    public Guid ProductTypeId { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    public ICollection<UsedComponent> ProductComponents { get; set; } = new List<UsedComponent>();
    public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}