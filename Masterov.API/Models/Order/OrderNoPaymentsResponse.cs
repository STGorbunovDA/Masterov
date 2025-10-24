using Masterov.API.Models.Customer;
using Masterov.API.Models.UsedComponent;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Order;

public class OrderNoPaymentsResponse
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public CustomerNoOrdersResponse CustomerNoOrders { get; set; }
    public List<UsedComponentResponse> UsedComponents { get; set; }
}