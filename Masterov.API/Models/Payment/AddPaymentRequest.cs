namespace Masterov.API.Models.Payment;

public class AddPaymentRequest
{
    public Guid OrderId { get; set; }
    public string MethodPayment { get; set; }
    public decimal Amount { get; set; }
    
    public string NameCustomer { get; set; }
    public string? EmailCustomer { get; set; }
    public string? PhoneCustomer { get; set; }
    
}