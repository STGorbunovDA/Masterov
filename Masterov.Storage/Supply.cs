using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Masterov.Storage;

public class Supply // Поставка
{
    [Key]
    public Guid SupplyId { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(Supplier))]
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; }

    [ForeignKey(nameof(ProductType))]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PriceSupply { get; set; }  // Цена за единицу

    [Required]
    public DateTime SupplyDate { get; set; } = DateTime.UtcNow;
}