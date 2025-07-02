using Masterov.Domain.Extension;

namespace Masterov.API.Models.ProductionOrder;

public class ProductionOrderNoCustomerNoComponentsNoPaymentsRequest
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public ProductionOrderStatus Status { get; set; }
    public string? Description { get; set; }
}