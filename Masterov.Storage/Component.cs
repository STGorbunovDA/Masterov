using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Masterov.Storage;

public class Component
{
    [Key]
    public Guid ComponentId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    [Required, ForeignKey(nameof(ProductType))]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    [Required, ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}