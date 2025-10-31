using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Masterov.Storage;

public class Warehouse
{
    [Key]
    public Guid WarehouseId { get; set; } = Guid.NewGuid();
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [ForeignKey(nameof(ComponentType))]
    public Guid ComponentTypeId { get; set; }
    public ComponentType ComponentType { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}