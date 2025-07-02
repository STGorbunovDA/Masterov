using Masterov.API.Models.Customer;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Payment;

public class PaymentRequest
{
    public Guid PaymentId { get; set; }
    public CustomerNewRequest Customer { get; set; }
    public PaymentMethod MethodPayment { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}