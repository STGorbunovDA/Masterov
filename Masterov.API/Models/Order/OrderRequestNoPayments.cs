using Masterov.API.Models.Component;
using Masterov.API.Models.Customer;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Order;

public class OrderRequestNoPayments
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public CustomerNoOrdersRequest CustomerNoOrders { get; set; }
    public List<ComponentRequest> Components { get; set; }
}