using Masterov.API.Models.Customer;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Payment;

public class PaymentNewRequest
{
    public Guid PaymentId { get; set; }
    public CustomerNoOrdersRequest Customer { get; set; }
    public PaymentMethod MethodPayment { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
}