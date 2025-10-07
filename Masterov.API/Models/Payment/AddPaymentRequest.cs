namespace Masterov.API.Models.Payment;

public class AddPaymentRequest
{
    public string MethodPayment { get; set; }
    public decimal Amount { get; set; }
    public Guid CustomerId { get; set; }
}