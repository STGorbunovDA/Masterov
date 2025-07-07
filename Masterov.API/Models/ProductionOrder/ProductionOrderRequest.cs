using Masterov.API.Models.Customer;
using Masterov.API.Models.Payment;
using Masterov.Domain.Extension;

namespace Masterov.API.Models.ProductionOrder;

public class ProductionOrderRequest
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public ProductionOrderStatus Status { get; set; }
    public string? Description { get; set; }
    public CustomerNoOrdersRequest CustomerNoOrders { get; set; }
    public List<ProductComponentRequest> Components { get; set; }
    public List<PaymentsNewCustomerRequest> PaymentsNoCustomer { get; set; }
    public decimal FullPriceFinishedProduct { get; set; }
}