using Masterov.API.Models.Customer;
using Masterov.API.Models.Payment;
using Masterov.API.Models.UsedComponent;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Order;

public class OrderResponse
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public CustomerNoOrdersResponse Customer { get; set; }
    public List<UsedComponentResponse> UsedComponents { get; set; }
    public List<PaymentsNewCustomerRequest> Payments { get; set; }
    public Guid FinishedProductId { get; set; }
    public decimal FullPriceFinishedProduct { get; set; }
}