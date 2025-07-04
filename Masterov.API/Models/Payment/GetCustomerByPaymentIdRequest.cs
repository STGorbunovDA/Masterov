namespace Masterov.API.Models.Payment;

public class GetCustomerByPaymentIdRequest
{
    public Guid PaymentId { get; set; }
}