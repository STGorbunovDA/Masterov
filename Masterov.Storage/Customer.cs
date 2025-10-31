using System.ComponentModel.DataAnnotations;

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

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}