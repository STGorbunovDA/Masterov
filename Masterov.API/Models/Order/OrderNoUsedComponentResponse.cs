using Masterov.API.Models.Customer;
using Masterov.API.Models.Payment;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.Order;

public class OrderNoUsedComponentResponse
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt  { get; set; }
    public DateTime? CompletedAt { get; set; }
    public OrderStatus Status { get; set; }
    public string? Description { get; set; }
    public CustomerNoOrdersResponse Customer { get; set; }
    public List<PaymentsNewCustomerRequest> Payments { get; set; }
    public Guid FinishedProductId { get; set; }
    public decimal FullPriceFinishedProduct { get; set; }
}