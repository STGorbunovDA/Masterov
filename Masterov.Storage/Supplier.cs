using System.ComponentModel.DataAnnotations;

namespace Masterov.Storage;

public class Supplier // Поставщик
{
    [Key]
    public Guid SupplierId { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}