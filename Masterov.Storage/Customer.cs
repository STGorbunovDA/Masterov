using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Masterov.Domain.Extension;

namespace Masterov.Storage;

public class Customer
{
    [Key]
    public Guid CustomerId { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime? UpdatedAt  { get; set; }

    public ICollection<ProductionOrder> Orders { get; set; } = new List<ProductionOrder>();
}