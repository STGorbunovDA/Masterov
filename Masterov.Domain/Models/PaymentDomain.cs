using Masterov.Domain.Extension;

namespace Masterov.Domain.Models;

public class PaymentDomain
{
    public Guid PaymentId { get; set; }
    public CustomerDomain Customer { get; set; }
    public PaymentMethod MethodPayment { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
}