using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Masterov.Storage;

public class ProductComponent
{
    [Key]
    public Guid ProductComponentId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    [Required, ForeignKey(nameof(ProductType))]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; } // Например: ножка

    [Required, ForeignKey(nameof(Warehouse))]
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } // С какого склада берем

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } // Сколько нужно
}