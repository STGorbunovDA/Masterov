using System.ComponentModel.DataAnnotations;

namespace Masterov.Storage;

public class Supplier
{
    [Key]
    public Guid SupplierId { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Name { get; set; }
    
    [Required, MaxLength(100)]
    public string Surname { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }
    
    [MaxLength(100)]
    public string? Email { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt  { get; set; }

    public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
}