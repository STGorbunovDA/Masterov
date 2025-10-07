using Masterov.API.Models.Payment;
using Masterov.API.Models.ProductComponent;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Order;

public class OrderRequestNoCustumer
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public List<ProductComponentRequest> Components { get; set; }
    public List<PaymentsNewCustomerRequest> PaymentsNoCustomer { get; set; }
}