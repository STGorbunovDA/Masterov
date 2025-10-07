using Masterov.Domain.Extension;

namespace Masterov.API.Models.Payment;

public class PaymentsNewCustomerRequest
{
    public Guid PaymentId { get; set; }
    public PaymentMethod MethodPayment { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
}