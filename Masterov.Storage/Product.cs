using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Masterov.Storage;

public class Product
{
    [Key] public Guid ProductId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [ForeignKey(nameof(ProductType))]
    public Guid ProductTypeId { get; set; }
    public ProductType? ProductType { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UploadedAt { get; set; }

    [Range(0, 30000)]
    public int Width { get; set; }  // в мм

    [Range(0, 30000)]
    public int Height { get; set; }  // в мм

    [Range(0, 30000)]
    public int Depth { get; set; }  // в мм

    public byte[]? Content { get; set; }
}