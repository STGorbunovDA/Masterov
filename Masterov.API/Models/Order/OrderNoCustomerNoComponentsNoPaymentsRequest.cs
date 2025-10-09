using Masterov.Domain.Extension;

namespace Masterov.API.Models.Order;

public class OrderNoCustomerNoComponentsNoPaymentsRequest
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
}