using Masterov.API.Models.Payment;
using Masterov.API.Models.UsedComponent;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Order;

public class OrderNoCustumerResponse
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public List<UsedComponentResponse> Components { get; set; }
    public List<PaymentsNewCustomerRequest> PaymentsNoCustomer { get; set; }
}