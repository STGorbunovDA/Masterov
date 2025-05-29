using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Masterov.Domain.Extension;

namespace Masterov.Storage;

public class ProductionOrder
{
    [Key]
    public Guid OrderId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    public Product Product { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }
    [Required]
    public ProductionOrderStatus Status { get; set; } = ProductionOrderStatus.Draft;

    public ICollection<ProductComponent> Components { get; set; } = new List<ProductComponent>();
}