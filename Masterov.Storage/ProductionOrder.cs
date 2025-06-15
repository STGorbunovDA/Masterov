using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Masterov.Domain.Extension;

namespace Masterov.Storage;

public class ProductionOrder
{
    [Key]
    public Guid OrderId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(FinishedProduct))]
    public Guid FinishedProductId { get; set; }

    public FinishedProduct FinishedProduct { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }
    
    [MaxLength(200)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(20)")]
    public ProductionOrderStatus Status { get; set; } = ProductionOrderStatus.Draft;

    public ICollection<ProductComponent> Components { get; set; } = new List<ProductComponent>();
    
    [Required, ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<OrderPayment> Payments { get; set; } = new List<OrderPayment>();

    [NotMapped]
    public decimal TotalPaid => Payments.Sum(p => p.Amount);

    [NotMapped]
    public bool IsPaidEnough => TotalPaid >= FinishedProduct.Price;
}