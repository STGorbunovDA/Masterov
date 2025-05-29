using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Masterov.Storage;

public class ProductComponent
{
    [Key]
    public Guid ProductComponentId { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    [ForeignKey(nameof(ProductType))]
    public Guid ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}