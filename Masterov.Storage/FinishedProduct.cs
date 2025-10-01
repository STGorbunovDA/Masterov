using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Masterov.Storage;

public class FinishedProduct
{
    [Key]
    public Guid FinishedProductId { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    [Range(0, 30000)]
    public int Width { get; set; }

    [Range(0, 30000)]
    public int Height { get; set; }

    [Range(0, 30000)]
    public int Depth { get; set; }

    public byte[]? Image { get; set; }

    public ICollection<ProductionOrder> Orders { get; set; } = new List<ProductionOrder>();
}