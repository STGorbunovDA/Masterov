namespace Masterov.API.Models.Payment;

public class UpdatePaymentRequest
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public string MethodPayment { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } 
}