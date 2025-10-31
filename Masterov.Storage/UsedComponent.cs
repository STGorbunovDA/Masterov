using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Masterov.Storage;

public class UsedComponent
{
    [Key]
    public Guid UsedComponentId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    [Required, ForeignKey(nameof(ComponentType))]
    public Guid ComponentTypeId { get; set; }
    public ComponentType ComponentType { get; set; }

    [Required, ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}