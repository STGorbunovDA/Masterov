namespace Masterov.API.Models.Payment;

public class GetPaymentsByOrderIdRequest
{
    public Guid OrderId { get; set; }
}