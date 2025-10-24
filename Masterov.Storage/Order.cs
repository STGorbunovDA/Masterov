using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Masterov.Domain.Extension;

namespace Masterov.Storage;

public class Order
{
    [Key]
    public Guid OrderId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(FinishedProduct))]
    public Guid FinishedProductId { get; set; }

    public FinishedProduct FinishedProduct { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(20)")]
    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public ICollection<UsedComponent> UsedComponents { get; set; } = new List<UsedComponent>();
    
    [Required, ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    
}