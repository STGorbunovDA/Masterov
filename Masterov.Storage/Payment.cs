using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Masterov.Domain.Extension;

namespace Masterov.Storage;

public class Payment
{
    [Key]
    public Guid PaymentId { get; set; } = Guid.NewGuid();

    [Required, ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public ProductionOrder Order { get; set; }

    [Required, ForeignKey(nameof(Customer))]
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public PaymentMethod MethodPayment { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt  { get; set; }
}